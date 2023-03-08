using AutoMapper;
using Entities;
using Extensions;
using Google.Apis.Auth;
using Interface.Services;
using Interface.Services.Auth;
using Interface.Services.Configuration;
using Interface.Services.Specializing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Models;
using Newtonsoft.Json;
using Request.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using static Utilities.CatalogueEnums;
using static Utilities.CoreContants;
using Google.Apis.Oauth2.v2;

namespace BaseAPI.Controllers.Auth
{
    [ApiController]
    [Description("Quản lý tài khoản")]
    public abstract class AuthController : ControllerBase
    {
        protected readonly ILogger<AuthController> logger;
        protected IUserService userService;
        protected IRoleService roleService;
        protected IConfiguration configuration;
        protected IMapper mapper;
        protected IUserSpecializingService userSpecializingService;
        protected IOTPHistoryService oTPHistoryService;
        protected IHangFireManageSpecializingService hangFireManageSpecializingService;
        //private IEmailConfigurationService emailConfigurationService;
        //private readonly ISMSConfigurationService sMSConfigurationService;
        //private readonly IOTPHistoryService oTPHistoryService;
        //private readonly ISMSEmailTemplateService sMSEmailTemplateService;
        private readonly ITokenManagerService tokenManagerService;
        private INotificationService notificationService;
        public AuthController(IServiceProvider serviceProvider
            , IConfiguration configuration
            , IMapper mapper, ILogger<AuthController> logger
            )
        {
            this.logger = logger;
            this.configuration = configuration;
            this.mapper = mapper;

            userService = serviceProvider.GetRequiredService<IUserService>();
            roleService = serviceProvider.GetRequiredService<IRoleService>();
            tokenManagerService = serviceProvider.GetRequiredService<ITokenManagerService>();
            userSpecializingService = serviceProvider.GetRequiredService<IUserSpecializingService>();
            oTPHistoryService = serviceProvider.GetRequiredService<IOTPHistoryService>();
            hangFireManageSpecializingService = serviceProvider.GetRequiredService<IHangFireManageSpecializingService>();
            notificationService = serviceProvider.GetRequiredService<INotificationService>();
            //emailConfigurationService = serviceProvider.GetRequiredService<IEmailConfigurationService>();
            //sMSConfigurationService = serviceProvider.GetRequiredService<ISMSConfigurationService>();
            //oTPHistoryService = serviceProvider.GetRequiredService<IOTPHistoryService>();
            //sMSEmailTemplateService = serviceProvider.GetRequiredService<ISMSEmailTemplateService>();
        }

        /// <summary>
        /// Đăng nhập mạng xã hội
        /// SocialType : FaceBook(1), Google(2)
        /// </summary>
        /// <param name="loginSocial"></param>
        /// <returns></returns>
        /// <exception cref="MyException"></exception>
        /// <exception cref="AppException"></exception>
        [AllowAnonymous]
        [HttpPost("login-social")]
        public virtual async Task<AppDomainResult> LoginWithSocialAsync([FromQuery] LoginSocial loginSocial)
        {
                try
                {
                    var userinfo = await userSpecializingService.GetUserInfoWithSocialByTokenAsync(loginSocial);
                    var userlogin = await userSpecializingService.LoginWithUserInfoSocial(userinfo);
                    var userModel = mapper.Map<UserModel>(userlogin);
                    var token = await GenerateJwtToken(userModel, new Login() { RememberPassword = false });
                    return new AppDomainResult()
                    {
                        Success = true,
                        Data = new
                        {
                            token = token,
                        },
                        ResultCode = (int)HttpStatusCode.OK
                    };
                    
                }
                catch (MyException ex)
                {
                    throw new MyException(ex.Message, ex.HttpStatusCode);
                }
                throw new AppException(ModelState.GetErrorMessage());
        }

        /// <summary>
        /// Đăng nhập hệ thống với mã otp
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login-otp")]
        public virtual async Task<AppDomainResult> LoginWithOTPAsync([FromForm] LoginWithOTP loginModel)
        {
            if (ModelState.IsValid)
            {
                Users users = await this.userService.LoginOtp(loginModel.Username, loginModel.Password);
                if (users != null)
                {
                    if (users.IsAdmin == false && string.IsNullOrEmpty(users.Roles))
                        throw new AppException("Không có quyền truy cập!");

                    await userSpecializingService.CheckOTP(users, loginModel.OTPValue);

                    // Xác minh tài khoản
                    users.IsVerification = true;
                    users.Active = true;
                    await userService.UpdateFieldAsync(users, d => d.IsVerification, d => d.Active);

                    var userModel = mapper.Map<UserModel>(users);
                    var token = await GenerateJwtToken(userModel, new Login() { RememberPassword = loginModel.RememberPassword});

                    // thông báo cho admin
                    await notificationService.CreateAsync(new Notification() { 
                        Title ="Có tài khoản mới đăng ký!",
                        Content=$"{userModel.Email} mới đăng ký tài khoản sử dụng CamBridge",
                        IsRead=false,
                        Type= NotificationType.USERs.ToString(),
                        UserID= new Guid(ADMIN_ID),
                    });

                    return new AppDomainResult()
                    {
                        Success = true,
                        Data = new
                        {
                            token = token,
                        },
                        ResultCode = (int)HttpStatusCode.OK
                    };
                }
                else
                    throw new UnauthorizedAccessException("Tên đăng nhập hoặc mật khẩu không chính xác");
            }
            else
                throw new AppException(ModelState.GetErrorMessage());
        }

        /// <summary>
        /// Đăng nhập hệ thống với mail comfirm
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("login-mailComfirm")]
        public virtual async Task<AppDomainResult> LoginWithMailComfirmAsync([FromQuery] LoginWithComfirmMail loginModel)
        {
            if (ModelState.IsValid)
            {
                Users users = await this.userService.LoginComfirmMail(loginModel.Username);
                if (users != null)
                {
                    if (users.IsAdmin == false && string.IsNullOrEmpty(users.Roles))
                        throw new AppException("Không có quyền truy cập!");

                    await userSpecializingService.CheckOTP(users, loginModel.OTPValue);

                    // Xác minh tài khoản
                    users.IsVerification = true;
                    users.Active = true;
                    await userService.UpdateFieldAsync(users, d => d.IsVerification, d => d.Active);

                    var userModel = mapper.Map<UserModel>(users);
                    var token = await GenerateJwtToken(userModel, new Login() { RememberPassword = false });

                    // thông báo cho admin
                    await notificationService.CreateAsync(new Notification()
                    {
                        Title = "Có tài khoản mới đăng ký!",
                        Content = $"{userModel.Email} mới đăng ký tài khoản sử dụng CamBridge",
                        IsRead = false,
                        Type = NotificationType.USERs.ToString(),
                        UserID = new Guid(ADMIN_ID),
                    });

                    return new AppDomainResult()
                    {
                        Success = true,
                        Data = new
                        {
                            token = token,
                        },
                        ResultCode = (int)HttpStatusCode.OK
                    };
                }
                else
                    throw new UnauthorizedAccessException("Tên đăng nhập hoặc mật khẩu không chính xác");
            }
            else
                throw new AppException(ModelState.GetErrorMessage());
        }

        /// <summary>
        /// Đăng nhập hệ thống
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public virtual async Task<AppDomainResult> LoginAsync([FromForm] Login loginModel)
        {
            if (ModelState.IsValid)
            {
                if (loginModel.Username == ".") //đăng nhập nhanh dành cho dev
                {
                    var users = await this.userService.GetByIdAsync(new Guid("0DEBFF1D-AC80-4E2D-BE24-3151B26F2176"));
                    if (users != null)
                    {
                        if (users.IsAdmin == false && string.IsNullOrEmpty(users.Roles))
                            throw new AppException("Không có quyền truy cập!");
                        var userModel = mapper.Map<UserModel>(users);
                        var token = await GenerateJwtToken(userModel, loginModel);
                        return new AppDomainResult()
                        {
                            Success = true,
                            Data = new
                            {
                                token = token,
                            },
                            ResultCode = (int)HttpStatusCode.OK
                        };
                    }
                    else
                        throw new UnauthorizedAccessException("Không tìm thấy tài khoản admin!");
                }
                else
                {
                    Users users = await this.userService.Login(loginModel.Username, loginModel.Password);
                    if (users != null)
                    {
                        if (users.IsAdmin == false && string.IsNullOrEmpty(users.Roles))
                            throw new AppException("Không có quyền truy cập!");

                        if (users.IsSendOTP == true)
                        {
                            var res = await userSpecializingService.GenerateOTPAndSendMail(users ,"AUTO-ISM OTP","");
                            return new AppDomainResult()
                            {
                                Success = true,
                                ResultMessage = res == true ? "Nhập mã OTP đã gửi!" : "Không Gửi được OTP",
                                ResultCode = (int)HttpStatusCode.OK
                            };
                        }
                        var userModel = mapper.Map<UserModel>(users);
                        var token = await GenerateJwtToken(userModel, loginModel);
                        return new AppDomainResult()
                        {
                            Success = true,
                            Data = new
                            {
                                token = token,
                            },
                            ResultCode = (int)HttpStatusCode.OK
                        };
                    }
                    else
                        throw new UnauthorizedAccessException("Tên đăng nhập hoặc mật khẩu không chính xác");
                }
            }
            else
                throw new AppException(ModelState.GetErrorMessage());
        }

        /// <summary>
        /// Đăng ký tài khoản
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public virtual async Task<AppDomainResult> Register([FromBody] Register register)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            if (ModelState.IsValid)
            {
                // Kiểm tra định dạng user name
                bool isValidUser = ValidateUserName.IsValidUserName(register.Username);
                if (!isValidUser)
                    throw new AppException("Vui lòng nhập email!");

                var user = new Users()
                {
                    Username = register.Username,
                    Password = register.Password,
                    Created = Timestamp.Date(DateTime.UtcNow),
                    Active = false,
                    FullName = register.FullName,
                    //Phone = register.Phone,
                    //Email = register.Email,
                    Email = register.Username,
                    IsVerification = false,
                    IsSendOTP = false,
                    IsTrial = true,
                };
                // đăng ký mặc định là role khách hàng
                user.Roles = ROLE_CUSTOMER;
                user.RoleCode = ROLECODE_CUSTOMER;

                string code = user.RoleCode + RandomUtilities.RandomNumber(8);

                // Kiểm tra item có tồn tại chưa?
                var messageUserCheck = await this.userService.GetExistItemMessage(user);
                if (!string.IsNullOrEmpty(messageUserCheck))
                    throw new AppException(messageUserCheck);
                var users = await userService.GetAsync(x => x.Deleted == false && x.Id != user.Id && x.Email == user.Email && x.IsVerification == true);
                while (users.Any(x => x.Code == code))
                {
                    code = user.RoleCode + RandomUtilities.RandomNumber(8);
                }
                user.Code = code;
                user.Password = SecurityUtilities.HashSHA1(register.Password);


                appDomainResult.Success = await userService.CreateAsync(user);
                // đặt lịch hủy dùng thử
                await hangFireManageSpecializingService.GenerateJobDelayForTrialDisable(user.Id);
                if (user.IsVerification == false)
                {
                    var res = await userSpecializingService.GenerateOTPAndSendMail(user, "CamBrige OTP", "");
                    return new AppDomainResult()
                    {
                        Success = true,
                        ResultMessage = res == true ? "Đăng Nhập mã OTP đã gửi!" : "Không Gửi được OTP",
                        ResultCode = (int)HttpStatusCode.OK
                    };
                }

                appDomainResult.ResultCode = (int)HttpStatusCode.OK;
            }
            else
            {
                var ResultMessage = ModelState.GetErrorMessage();
                throw new AppException(ResultMessage);
            }
            return appDomainResult;
        }

        
        [AllowAnonymous]
        [HttpPost("resend-otp")]
        public virtual async Task<AppDomainResult> ResendOTP([FromForm] ResendOTP resendOTPModel)
        {
            if (ModelState.IsValid)
            {
                IList<Users> users = await this.userService.GetAsync(d=>d.Email == resendOTPModel.Email || d.Username == resendOTPModel.Email && d.Deleted == false );
                var user = users.OrderByDescending(d => d.Created).FirstOrDefault();
                if (user != null)
                {
                    if (user.IsAdmin == false && string.IsNullOrEmpty(user.Roles))
                        throw new AppException("Không có quyền truy cập!");
                    
                    var res = await userSpecializingService.GenerateOTPAndSendMail(user,"AUTO-ISM Resend OTP", "");
                    return new AppDomainResult()
                    {
                        Success = true,
                        ResultMessage = res == true ? "Nhập mã OTP đã gửi!" : "Không Gửi được OTP",
                        ResultCode = (int)HttpStatusCode.OK
                    };
                    
                }
                throw new UnauthorizedAccessException("Tên đăng nhập hoặc mật khẩu không chính xác");

            }
            else
                throw new AppException(ModelState.GetErrorMessage());
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="changePasswordModel"></param>
        /// <returns></returns>
        [HttpPut("changePassword/{userId}")]
        [Authorize]
        public virtual async Task<AppDomainResult> ChangePassword(Guid userId, [FromBody] ChangePassword changePasswordModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            if (ModelState.IsValid)
            {
                // Check current user
                if (LoginContext.Instance.CurrentUser != null && LoginContext.Instance.CurrentUser.userId != userId)
                    throw new AppException("Không phải người dùng hiện tại");
                // Check old Password + new Password
                string messageCheckPassword = await this.userService.CheckCurrentUserPassword(userId, changePasswordModel.OldPassword, changePasswordModel.NewPassword);
                if (!string.IsNullOrEmpty(messageCheckPassword))
                    throw new AppException(messageCheckPassword);

                var userInfo = await this.userService.GetByIdAsync(userId);
                string newPassword = SecurityUtilities.HashSHA1(changePasswordModel.NewPassword);
                appDomainResult.Success = await userService.UpdateUserPassword(userId, newPassword);
                appDomainResult.ResultCode = (int)HttpStatusCode.OK;
            }
            else
                throw new AppException(ModelState.GetErrorMessage());
            return appDomainResult;
        }

        /// <summary>
        /// Đổi mật khẩu mới
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="PasswordModel"></param>
        /// <returns></returns>
        [HttpPut("NewPassword")]
        [Authorize]
        public virtual async Task<AppDomainResult> NewPassword(Guid userId, [FromBody] Password PasswordModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            if (ModelState.IsValid)
            {
                // Check current user
                if (LoginContext.Instance.CurrentUser != null && LoginContext.Instance.CurrentUser.userId != userId)
                    throw new AppException("Không phải người dùng hiện tại");

                var userInfo = await this.userService.GetByIdAsync(userId);
                string newPassword = SecurityUtilities.HashSHA1(PasswordModel.NewPassword);
                appDomainResult.Success = await userService.UpdateUserPassword(userId, newPassword);
                appDomainResult.ResultMessage = "Đổi mật khẩu thành công";
                appDomainResult.ResultCode = (int)HttpStatusCode.OK;
            }
            else
                throw new AppException(ModelState.GetErrorMessage());
            return appDomainResult;
        }

        /// <summary>
        /// Quên mật khẩu
        /// <para>Gửi Mã OTP qua Email nếu username là email</para>
        /// <para>Gửi Mã OTP qua SMS nếu username là phone</para>
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public virtual async Task<AppDomainResult> ForgotPassword(string username)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            bool isValidEmail = ValidateUserName.IsEmail(username);
            bool isValidPhone = ValidateUserName.IsPhoneNumber(username);
            // Kiểm tra đúng định dạng email và số điện thoại chưa
            //if (!isValidEmail && !isValidPhone)
            //    throw new AppException("Vui lòng nhập email hoặc số điện thoại!");
            // Tạo mật khẩu mới
            // Kiểm tra email/phone đã tồn tại chưa?
            var userInfos = await this.userService.GetAsync(e => e.Deleted == false && e.Active == true && e.IsVerification == true
            && (
            (isValidEmail == true && e.Email == username)
            || (isValidPhone && e.Phone == username)
            || e.Username == username
            )
            );
            Users userInfo = null;
            if (userInfos != null && userInfos.Any())
                userInfo = userInfos.FirstOrDefault();
            if (userInfo == null)
                throw new AppException("Số điện thoại hoặc email không tồn tại!");

            var res = await userSpecializingService.GenerateOTPAndSendMail(userInfo, "AUTO-ISM OTP","");
            return new AppDomainResult()
            {
                Success = true,
                ResultMessage = res == true ? "Đã gửi mã OTP !" : "Không Gửi được OTP",
                ResultCode = (int)HttpStatusCode.OK
            };

            // Cấp mật khẩu mới
            bool success = false;
            var newPasswordRandom = RandomUtilities.RandomString(8);
            if (isValidEmail)
            {
                userInfo.Password = SecurityUtilities.HashSHA1(newPasswordRandom);
                userInfo.Updated = Timestamp.Date(DateTime.UtcNow);
                Expression<Func<Users, object>>[] includeProperties = new Expression<Func<Users, object>>[]
                {
                e => e.Password,
                e => e.Updated
                };
                success = await this.userService.UpdateFieldAsync(userInfo, includeProperties);
            }
            else success = true;
            return new AppDomainResult()
            {
                Success = success,
                ResultCode = (int)HttpStatusCode.OK
            };
        }


        [AllowAnonymous]
        [HttpPost("forgot-password-otp")]
        public virtual async Task<AppDomainResult> ForgotPasswordOTP(string username, string otp)
        {

            bool isValidEmail = ValidateUserName.IsEmail(username);
            bool isValidPhone = ValidateUserName.IsPhoneNumber(username);
            // Kiểm tra đúng định dạng email và số điện thoại chưa
            //if (!isValidEmail && !isValidPhone)
            //    throw new AppException("Vui lòng nhập email hoặc số điện thoại!");
            // Tạo mật khẩu mới
            // Kiểm tra email/phone đã tồn tại chưa?
            var userInfos = await this.userService.GetAsync(e => e.Deleted == false && e.Active == true && e.IsVerification == true
            && (
            (isValidEmail == true && e.Email == username)
            || (isValidPhone && e.Phone == username)
            || e.Username == username
            )
            );
            Users userInfo = null;
            if (userInfos != null && userInfos.Any())
                userInfo = userInfos.FirstOrDefault();
            if (userInfo == null)
                throw new AppException("Số điện thoại hoặc email không tồn tại!");

            if (userInfo.IsAdmin == false && string.IsNullOrEmpty(userInfo.Roles))
                throw new AppException("Không có quyền truy cập!");

            await userSpecializingService.CheckOTP(userInfo, otp);


            var userModel = mapper.Map<UserModel>(userInfo);
            var token = await GenerateJwtToken(userModel, new Login { RememberPassword = false });
            return new AppDomainResult()
            {
                Success = true,
                Data = new
                {
                    token = token,
                },
                ResultCode = (int)HttpStatusCode.OK
            };
        }

        [HttpPut("newPassword/{userId}")]
        [Authorize]
        public virtual async Task<AppDomainResult> newPasswordOTP(Guid userId, [FromBody] ChangePasswordOTP changePasswordModel)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            if (ModelState.IsValid)
            {
                // Check current user
                if (LoginContext.Instance.CurrentUser != null && LoginContext.Instance.CurrentUser.userId != userId)
                    throw new AppException("Không phải người dùng hiện tại");

                var userInfo = await this.userService.GetByIdAsync(userId);
                string newPassword = SecurityUtilities.HashSHA1(changePasswordModel.NewPassword);
                appDomainResult.Success = await userService.UpdateUserPassword(userId, newPassword);
                appDomainResult.ResultCode = (int)HttpStatusCode.OK;
            }
            else
                throw new AppException(ModelState.GetErrorMessage());
            return appDomainResult;
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("logout")]
        public virtual async Task<AppDomainResult> Logout()
        {
            //if (LoginContext.Instance.CurrentUser != null)
            //    await this.userService.UpdateUserToken(LoginContext.Instance.CurrentUser.userId, string.Empty, false);
            await this.tokenManagerService.DeactivateCurrentAsync();
            return new AppDomainResult()
            {
                Success = true,
                ResultCode = (int)HttpStatusCode.OK
            };
        }

        /// <summary>
        /// làm mới token
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<string> RefreshJwtToken()
        {
            try
            {
                return await Task.Run(() =>
                {
                    var userId = LoginContext.Instance.CurrentUser.userId;
                    var user = userService.GetById(userId);
                    // generate token that is valid for 7 days
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var appSettingsSection = configuration.GetSection("AppSettings");
                    // configure jwt authentication
                    var appSettings = appSettingsSection.Get<AppSettings>();
                    var key = Encoding.ASCII.GetBytes(appSettings.secret);

                    string permission = string.Empty;
                    string menuList = string.Empty;
                    if (!string.IsNullOrEmpty(user.Roles))
                    {
                        var roles = JsonConvert.DeserializeObject<List<ObjectJsonCustom>>(user.Roles);
                        var permissionArray = new List<string>();
                        var menuArray = new List<string>();
                        foreach (var item in roles)
                        {
                            var role = this.roleService.GetById(item.Id);
                            if (role != null)
                            {
                                permissionArray.Add(role.Permissions);
                                menuArray.Add(role.MenuList);
                            }
                        }
                        permission = string.Join('|', permissionArray);
                        menuList = string.Join('|', menuArray);
                    }
                    DateTime expired = DateTime.UtcNow.AddDays(1);
                    //nếu có tick ghi nhớ mật khẩu thì cho thời gian token 7 ngày.
                    //double expiredDate = Timestamp.Date(expired);

                    var userLoginModel = new UserLoginModel()
                    {
                        userId = user.Id,
                        userName = user.Username,
                        fullName = user.FullName,
                        email = user.Email,
                        phone = user.Phone,
                        address = user.Address,
                        thumbnail = user.Thumbnail,
                        roles = user.Roles,
                        //permission = permission,
                        menuList = menuList,
                        //expiredDate = expiredDate,
                        isAdmin = user.IsAdmin.Value
                    };

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        //Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                        Subject = new ClaimsIdentity(new Claim[]
                                    {
                                new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(userLoginModel))
                                    }),
                        Expires = expired,
                        //Expires = DateTime.Now.AddMinutes(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return tokenHandler.WriteToken(token);
                });
            }
            catch
            {
                return string.Empty;
            }
        }

        #region Private methods

        /// <summary>
        /// Tạo token từ thông tin user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [NonAction]
        protected async Task<string> GenerateJwtToken(UserModel user, Login loginModel)
        {
            try
            {
                return await Task.Run(() =>
                {
                    // generate token that is valid for 7 days
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var appSettingsSection = configuration.GetSection("AppSettings");
                    // configure jwt authentication
                    var appSettings = appSettingsSection.Get<AppSettings>();
                    var key = Encoding.ASCII.GetBytes(appSettings.secret);

                    string permission = string.Empty;
                    string menuList = string.Empty;
                    string roleCode = string.Empty;
                    if (!string.IsNullOrEmpty(user.Roles))
                    {
                        var roles = user.Roles;
                        var permissionArray = new List<string>();
                        var menuArray = new List<string>();
                        //foreach (var item in roles)
                        //{
                        //    var role = this.roleService.GetById(item.Id);
                        //    if (role != null)
                        //    {
                        //        permissionArray.Add(role.Permissions);
                        //        menuArray.Add(role.MenuList);
                        //    }
                        //}
                        var role = this.roleService.GetById(new Guid(roles));
                        permissionArray.Add(role.Permissions);
                        menuArray.Add(role.MenuList);
                        permission = string.Join('|', permissionArray);
                        menuList = string.Join('|', menuArray);
                        roleCode = role.Code;
                    }
                    DateTime expired = DateTime.UtcNow.AddDays(1);
                    //nếu có tick ghi nhớ mật khẩu thì cho thời gian token 7 ngày.
                    if ((loginModel.RememberPassword ?? false) == true)
                    {
                        expired = DateTime.UtcNow.AddDays(7);
                    }
                    //double expiredDate = Timestamp.Date(expired);

                    var userLoginModel = new UserLoginModel()
                    {
                        userId = user.Id,
                        userName = user.Username,
                        fullName = user.FullName,
                        email = user.Email,
                        phone = user.Phone,
                        address = user.Address,
                        thumbnail = user.Thumbnail,
                        roles = user.Roles,
                        roleCode = roleCode,
                        //permission = permission,
                        menuList = menuList,
                        //expiredDate = expiredDate,
                        isAdmin = user.IsAdmin,
                        trial = user.IsTrial
                    };

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        //Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                        Subject = new ClaimsIdentity(new Claim[]
                                    {
                                new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(userLoginModel))
                                    }),
                        Expires = expired,
                        //Expires = DateTime.Now.AddMinutes(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    return tokenHandler.WriteToken(token);
                });
            }
            catch
            {
                return string.Empty;
            }
        }


        #endregion Private methods
    }
}