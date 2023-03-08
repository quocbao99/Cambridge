using Entities;
using Entities.Search;
using Extensions;
using Interface.Services;
using Interface.Services.Configuration;
using Interface.Services.Specializing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using Request.RequestCreate;
using Request.RequestUpdate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Utilities;
using static Utilities.CatalogueEnums;
using static Utilities.CoreContants;

namespace BaseAPI.Controllers
{
    /// <summary>
    /// Quản lý người dùng
    /// </summary>
    [Route("api/user")]
    [ApiController]
    [Description("Quản lý nhân viên")]
    [Authorize]
    public class UserController : BaseController<Users, UserModel, UserCreate, UserUpdate, UserSearch>
    {
        private IRoleService roleService;
        private IUserService userService;
        protected IUserSpecializingService userSpecializingService;
        private IEmailConfigurationService emailConfigurationService;
        public UserController(IServiceProvider serviceProvider, ILogger<BaseController<Users, UserModel, UserCreate, UserUpdate, UserSearch>> logger
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.emailConfigurationService = serviceProvider.GetRequiredService<IEmailConfigurationService>();
            this.domainService = serviceProvider.GetRequiredService<IUserService>();
            this.roleService = serviceProvider.GetRequiredService<IRoleService>();
            this.userService = serviceProvider.GetRequiredService<IUserService>();
            this.userSpecializingService = serviceProvider.GetRequiredService<IUserSpecializingService>();
        }

        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="itemModel"></param>
        /// <returns></returns>
        [HttpPost()]
        [AppAuthorize]
        [Description("Thêm mới nhân viên")]
        public override async Task<AppDomainResult> AddItem([FromBody] UserCreate itemModel)
        {
            if (ModelState.IsValid)
            {
                var item = mapper.Map<Users>(itemModel);
                if (item != null)
                {
                    //role from request
                    var role = await roleService.GetByIdAsync((Guid)itemModel.RoleId);
                    if (role == null)
                        throw new AppException("Quyền không tồn tại");
                    
                    IList<Users> users = await this.domainService.GetAsync(d=>(d.Username == item.Username || d.Email == item.Email || d.Phone == item.Phone) && (d.IsVerification == true && d.Deleted== false) );
                    if (users.Any(x => x.Username == item.Username && x.Active == true && x.IsVerification == true && x.Deleted == false))
                        throw new AppException("Tên đăng nhập đã tồn tại trong hệ thống!");
                    if (users.Any(x => x.Phone == item.Phone && x.Active == true && x.IsVerification == true && x.Deleted == false))
                        throw new AppException("Số điện thoại đã tồn tại trong hệ thống!");
                    if (users.Any(x => x.Email == item.Email && x.Active == true && x.IsVerification == true && x.Deleted == false))
                        throw new AppException("Email đã tồn tại trong hệ thống!");

                    item.Password = string.IsNullOrEmpty(item.Password) ? SecurityUtilities.HashSHA1("23312331") : SecurityUtilities.HashSHA1(item.Password);
                    item.Username = (item.Username ?? item.Phone).Replace(" ", "").Trim();
                    if (!LoginContext.Instance.CurrentUser.isAdmin )
                    {
                        throw new AppException("Không có quyền tạo quản lý");
                    }
                    item.RoleCode = role.Code;
                    string code = role.Code + RandomUtilities.RandomNumber(8);
                    while (users.Any(x => x.Code == code))
                    {
                        code = role.Code + RandomUtilities.RandomNumber(8);
                    }
                    item.Created = Timestamp.Now();
                    item.IsVerification = true; // đã xác thực
                    item.IsTrial = false;
                    item.IsSendOTP= false;
                    item.IsAdmin = false;
                    item.IsLock = false;
                    item.Code = code;
                    item.Roles = role.Id.ToString();
                    var success = await this.userService.CreateAsync(item);
                    // gửi mã OTP qua mail
                    string Tos = item.Email.Equals(String.Empty) ? item.Username : item.Email;
                    if (String.Empty.Contains(Tos)) throw new Exception("Lỗi không thể gửi mail");
                    string[] CCs = { };
                    string[] BCCs = { };
                    await emailConfigurationService.SendMail("Thông tin tài khoản", Tos, CCs, BCCs, new EmailContent { isHtml = true, content = $" TK: {item.Email} </br> MK:{ item.Password}", attachments = null });
                    
                    if (!success)
                        throw new Exception("Lỗi trong quá trình xử lý");
                    return new AppDomainResult()
                    {
                        ResultCode = (int)HttpStatusCode.OK,
                        ResultMessage = "Thêm mới người dùng thành công",
                        Success = true
                    };
                }
                else
                    throw new AppException("Item không tồn tại");
            }
            throw new AppException(ModelState.GetErrorMessage());
        }


        /// <summary>
        /// Cập nhật thông tin User
        /// </summary>
        /// <param name="itemModel"></param>
        /// <returns></returns>
        [HttpPut]
        [AppAuthorize]
        [Description("Cập nhật User")]
        public override async Task<AppDomainResult> UpdateItem([FromBody] UserUpdate itemModel)
        {
            if (ModelState.IsValid)
            {
                var item = mapper.Map<Users>(itemModel);
                if (item != null)
                {
                    Users user = await domainService.GetByIdAsync(item.Id);
                    if (user == null) throw new Exception("Không tìm thấy thông tin người dùng");
                    if (!user.IsAdmin.Value)
                    {
                        
                        IList<Users> listUser = await domainService.GetAsync(x => x.Id != item.Id && x.Deleted == false && x.Active == true && x.IsVerification == true);

                        if (listUser.Any(x => x.Email == item.Email) && !item.Email.Equals("") )
                            throw new AppException("Email đã tồn tại trong hệ thống!");
                        if (itemModel.Email is not null || itemModel.Email == "") {
                            bool isValidEmail = ValidateUserName.IsEmail(itemModel.Email);
                            
                            if(!isValidEmail) throw new AppException("Sai định dạng mail !");

                            user.EmailTmp = itemModel.Email;
                            await domainService.UpdateFieldAsync(user, d => d.EmailTmp);

                            user.Email = itemModel.Email;
                            user.Username = itemModel.Username;
                            var res = await userSpecializingService.GenerateOTPAndSendMail(user, "AUTO-ISM OTP", "");
                            return new AppDomainResult()
                            {
                                Success = true,
                                ResultMessage = res == true ? "Nhập mã OTP đã gửi!" : "Không Gửi được OTP",
                                ResultCode = (int)HttpStatusCode.OK
                            };
                        }
                        if (listUser.Any(x => x.Username == itemModel.Username))
                            throw new AppException("UserName đã tồn tại trong hệ thống!");
                        if (listUser.Any(x => x.Phone == itemModel.Phone && !String.Empty.Equals("")))
                            throw new AppException("Số điện thoại đã tồn tại trong hệ thống!");

                        item.Password = user.Password;
                    }
                    if (item.Roles == null || String.Empty.Equals(item.Roles))
                        item.Roles = user.Roles;
                    if (item.FullName is null || String.Empty.Equals(item.FullName))
                        item.FullName = user.FullName;

                    if (item.Phone is null || String.Empty.Equals(item.Phone))
                        item.Phone = user.Phone;

                    if (item.Birthday is null || String.Empty.Equals(item.Birthday))
                        item.Birthday = user.Birthday;

                    if (item.Gender is null || String.Empty.Equals(item.Gender))
                        item.Gender = user.Gender;

                    if (item.Roles is null || String.Empty.Equals(item.Roles))
                        item.Roles = user.Roles;
                    if (item.IsTrial is null )
                        item.IsTrial = user.IsTrial;

                    

                    if (itemModel.OneSignalID is null)
                        item.OneSignalID = user.OneSignalID;

                    bool success = await this.domainService.UpdateFieldAsync(item
                        , d=> d.FullName
                        , d=> d.Phone
                        , d=>d.Birthday
                        , d=> d.Gender
                        , d=> d.Roles
                        , d=> d.IsTrial
                        , d => d.Thumbnail
                        , d => d.OneSignalID);
                    if (!success)
                        throw new Exception("Lỗi trong quá trình xử lý");
                    return new AppDomainResult() { ResultCode = (int)HttpStatusCode.OK, ResultMessage = "Cập nhật tài khoản người dùng thành công!", Success = true };
                }
                else
                    throw new KeyNotFoundException("Item không tồn tại");
            }
            else
                throw new AppException(ModelState.GetErrorMessage());
        }

        [HttpPut("newEmail/{userId}")]
        [Authorize]
        public virtual async Task<AppDomainResult> newEmail(Guid userId, string Otp)
        {
            AppDomainResult appDomainResult = new AppDomainResult();
            if (ModelState.IsValid)
            {
                // Check current user
                if (LoginContext.Instance.CurrentUser != null && LoginContext.Instance.CurrentUser.userId != userId)
                    throw new AppException("Không phải người dùng hiện tại");
                var user = await domainService.GetByIdAsync(userId);
                if (user == null)
                    throw new AppException("Không tìm thấy thông tin người dùng");
                await userSpecializingService.CheckOTP(user, Otp);

                user.Email = user.EmailTmp;
                await domainService.UpdateFieldAsync(user, d => d.Email);
                appDomainResult.ResultCode = (int)HttpStatusCode.OK;
            }
            else
                throw new AppException(ModelState.GetErrorMessage());
            return appDomainResult;
        }

        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        /// <param name="baseSearch"></param>
        /// <returns></returns>
        [HttpGet]
        [AppAuthorize]
        [Description("Danh sách nhân viên")]
        public override async Task<AppDomainResult> Get([FromQuery] UserSearch baseSearch)
        {
            if (ModelState.IsValid)
            {
                PagedList<Users> pagedData = await this.domainService.GetPagedListData(baseSearch);
                PagedList<UserModel> pagedDataModel = mapper.Map<PagedList<UserModel>>(pagedData);
                if (pagedDataModel.Items == null)
                {
                    return new AppDomainResult
                    {
                        Data = pagedDataModel,
                        Success = true,
                        ResultCode = (int)HttpStatusCode.OK
                    };
                }
                
                return new AppDomainResult
                {
                    Data = pagedDataModel,
                    Success = true,
                    ResultCode = (int)HttpStatusCode.OK
                };
            }
            else
                throw new AppException(ModelState.GetErrorMessage());
        }
        /// <summary>
        /// Xem thông tin chi tiết
        /// </summary>
        [HttpGet("{id}")]
        [AppAuthorize]
        [Description("Xem thông tin chi tiết")]
        public override async Task<AppDomainResult> GetById(Guid id)
        {
            if (ModelState.IsValid)
            {
                var user = await this.domainService.GetByIdAsync(id);
                var userModel = mapper.Map<UserModel>(user);

                return new AppDomainResult
                {
                    Data = userModel,
                    Success = true,
                    ResultCode = (int)HttpStatusCode.OK
                };
            }
            else
                throw new AppException(ModelState.GetErrorMessage());
        }
        

    }
}