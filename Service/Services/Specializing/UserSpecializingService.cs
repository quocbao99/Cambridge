using Entities;
using Entities.Configuration;
using Extensions;
using Google.Apis.Auth;
using Interface.Services;
using Interface.Services.Configuration;
using Interface.Services.Specializing;
using Microsoft.Extensions.Configuration;
using Models;
using Request.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities;
using static Utilities.CoreContants;

namespace Service.Services.Specializing
{
    public class UserSpecializingService : IUserSpecializingService
    {
        private IConfiguration configuration;

        private IUserService userService;
        private IOTPHistoryService oTPHistoryService;
        private IOTPHistoriesSpecializingService oTPHistoriesSpecializingService;
        private IHangFireManageSpecializingService hangFireManageSpecializingService;
        private IEmailConfigurationService emailConfigurationService;
        public UserSpecializingService(
            IConfiguration configuration
            , IUserService userService
            , IOTPHistoriesSpecializingService oTPHistoriesSpecializingService
            , IOTPHistoryService oTPHistoryService
            , IHangFireManageSpecializingService hangFireManageSpecializingService
            , IEmailConfigurationService emailConfigurationService
            ) {

            this.configuration = configuration;
            this.userService = userService;
            this.oTPHistoryService = oTPHistoryService;
            this.oTPHistoriesSpecializingService = oTPHistoriesSpecializingService;
            this.hangFireManageSpecializingService = hangFireManageSpecializingService;
            this.emailConfigurationService = emailConfigurationService;
        }

        /// <summary>
        /// Kiểm tra mã OTP hợp lệ
        /// </summary>
        /// <param name="user"></param>
        /// <param name="OTPCheck"></param>
        /// <returns></returns>
        public async Task<bool> CheckOTP(Users user, string OTPCheck)
        {
            var otp = await oTPHistoryService.GetAsync(d => d.UserId == user.Id
                    && d.Deleted == false
                    && d.Active == true
                    && d.Status == (int)OTPStatus.UnExpired
                    && d.ExpiredDate >= Timestamp.Now()
                    );
            if (otp.Count() != 1) throw new Exception("Không tìm thấy mã OTP");
            if (otp.FirstOrDefault().OtpValue != OTPCheck) { throw new Exception("Nhập sai mã OTP nhập lại!"); }
            return true;
        }

        /// <summary>
        /// Tạo Mã OTP cho User và gửi mail đến User đó
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> GenerateOTPAndSendMail(Users user , string title, string content)
        {
            try { 
            // tạo mã OTP sau 10 phút hủy mã OTP
            var otps = await oTPHistoryService.GetAsync(d => d.UserId == user.Id
            && d.Deleted == false
            /*&& d.ExpiredDate <= Timestamp.Now()*/
            && d.Status == (int)OTPStatus.UnExpired);
            if (otps.Count() == 1) {
                await oTPHistoriesSpecializingService.ExpiredOTP(otps[0].Id);
            }
            var oTP = await oTPHistoriesSpecializingService.GenerateOTP(user);
            var res = await hangFireManageSpecializingService.GenerateJobDelayForExpiredOTP(oTP.Id);
            if (res == false) throw new Exception("không lên lịch tự động hủy mã OTP");
            // gửi mã OTP qua mail
            string Tos = user.Email.Equals(String.Empty) ? user.Username : user.Email;
            if(String.Empty.Contains(Tos)) throw new Exception("Lỗi không thể gửi mail");
            string[] CCs = { };
            string[] BCCs = { };

            // tạo luồng gửi mail
            //Thread SendMail = new Thread(() => { ThreadSendMail(Tos, CCs, BCCs, oTP); });
            await emailConfigurationService.SendMail(title, Tos, CCs, BCCs, new EmailContent { isHtml = true, content = oTP .OtpValue, attachments = null});
            //SendMail.Start();
            return true;
            }
            catch (Exception ex) {
                throw new Exception("Lỗi không thể gửi mail");
            }
        }
        public void ThreadSendMail(string Tos, string[] CCs, string[] BCCs, OTPHistories oTP) {
            emailConfigurationService.SendMail(SubjectMailLoginWithOTP, Tos, CCs, BCCs, new EmailContent { isHtml = true, content = oTP.OtpValue, attachments = null });
        }

        public async Task<UserInfoOfSocialModel> GetUserInfoWithSocialByTokenAsync(LoginSocial loginSocial) {
            try
            {
                if (string.IsNullOrEmpty(loginSocial.Token))
                {
                    throw new MyException("Không lấy được token", HttpStatusCode.BadRequest);
                }
                UserInfoOfSocialModel userInfoOfSocialModel = new UserInfoOfSocialModel();
                if (loginSocial.SocialType == (int)SocialType.FaceBook){
                    //var fb = new FacebookClient();
                    //fb.AccessToken = loginSocial.Token;

                    //    dynamic data = fb.Get("me?fields=id,name,email,link,first_name,last_name,gender,locale,picture,age_range");
                    //    if (data == null)
                    //    {
                    //        throw new MyException("Không lấy được Dữ liệu từ facebook", HttpStatusCode.BadRequest);

                    //    }
                    //    userInfoOfSocialModel.SocialId = data.id;
                    //    userInfoOfSocialModel.Name = data.name;
                    //    userInfoOfSocialModel.Email = data.email;
                    //    userInfoOfSocialModel.Gender = data.gender;
                    throw new MyException("Tính năng đã tắt không được hỗ trợ", HttpStatusCode.BadRequest);
                }
                if (loginSocial.SocialType == (int)SocialType.Google) {
                    GoogleConfiguration googleConfiguration = new GoogleConfiguration();
                    //lấy cấu hình từ json gán vào
                    configuration.GetSection("googleConfiguration").Bind(googleConfiguration);
                    var payload = await GoogleJsonWebSignature.ValidateAsync(loginSocial.Token, new GoogleJsonWebSignature.ValidationSettings()
                    {
                        Audience = new[] { googleConfiguration.ClientID }
                    });
                    if (payload == null)
                    {
                        throw new MyException("Không lấy được Dữ liệu từ google", HttpStatusCode.BadRequest);
                    }
                    userInfoOfSocialModel.SocialId = payload.Subject;
                    userInfoOfSocialModel.Name = payload.Name;
                    userInfoOfSocialModel.Email = payload.Email;
                }
                if (userInfoOfSocialModel.Email == null || userInfoOfSocialModel.Email.Equals(String.Empty)) throw new MyException("Không tìm thấy email", HttpStatusCode.BadRequest);
                if (userInfoOfSocialModel.SocialId == null || userInfoOfSocialModel.SocialId.Equals(String.Empty)) throw new MyException("UserID Facebook Sai định dạng", HttpStatusCode.BadRequest);
                userInfoOfSocialModel.SocialType = loginSocial.SocialType;
                return userInfoOfSocialModel;
            }
            catch (MyException ex)
            {
                throw new MyException(ex.Message, ex.HttpStatusCode);
            }
        }
        public async Task<Users> LoginWithUserInfoSocial(UserInfoOfSocialModel userInfoOfSocial) {
            try {
                IList<Users> users = await this.userService.GetAsync(
            d => d.Username == userInfoOfSocial.Email
            && d.SocialId == userInfoOfSocial.SocialId
            && d.SocialType == (int)userInfoOfSocial.SocialType
            && d.Deleted == false
            );
                if (users.Count() > 1 || users == null) throw new MyException("Lỗi hệ thống", HttpStatusCode.InternalServerError);
                if (users.Count() == 0)
                {
                    // chưa có tài khoản
                    // kiểm tra username, mail, sdt có trùng không
                    var user = new Users()
                    {
                        Username = userInfoOfSocial.Email,
                        FullName = userInfoOfSocial.Name,
                        Password = "987654321",
                        Created = Timestamp.Date(DateTime.UtcNow),
                        Phone = "",
                        Email = userInfoOfSocial.Email,
                        IsVerification = true, // đã xác nhận
                        IsSendOTP = false,
                        SocialId = userInfoOfSocial.SocialId // Id Của User trên google
                        ,
                        SocialType = userInfoOfSocial.SocialType
                    };
                    // đăng ký mặc định là role khách hàng
                    user.Roles = ROLE_CUSTOMER;
                    user.IsTrial = true; // dùng thử
                    // Kiểm tra item có tồn tại chưa?
                    var messageUserCheck = await this.userService.GetExistItemMessage(user);
                    if (!string.IsNullOrEmpty(messageUserCheck))
                        throw new AppException(messageUserCheck);
                    user.Password = SecurityUtilities.HashSHA1(user.Password);
                    var res = await userService.CreateAsync(user);
                    // đặt lịch hủy dùng thử
                    await hangFireManageSpecializingService.GenerateJobDelayForTrialDisable(user.Id);
                    if (res == false) throw new MyException("Đăng ký thất bại", HttpStatusCode.InternalServerError);

                    return user;
                }
                if (users.Count() == 1)
                {
                    var user = users.FirstOrDefault();
                    if (user.IsAdmin == false && string.IsNullOrEmpty(user.Roles))
                        throw new AppException("Không có quyền truy cập!");
                    return user;
                }
                throw new MyException("Lỗi hệ thống", HttpStatusCode.InternalServerError);
            } catch (MyException ex) {
                throw new MyException(ex.Message, ex.HttpStatusCode);
            }
        }

    }
}
