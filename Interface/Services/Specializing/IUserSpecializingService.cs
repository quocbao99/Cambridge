using Entities;
using Models;
using Request.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Services.Specializing
{
    public interface IUserSpecializingService
    {
        public Task<bool> GenerateOTPAndSendMail(Users user, string title, string content);
        public Task<bool> CheckOTP(Users user, string OTPCheck);
        public Task<UserInfoOfSocialModel> GetUserInfoWithSocialByTokenAsync(LoginSocial loginSocial);
        public Task<Users> LoginWithUserInfoSocial(UserInfoOfSocialModel userInfoOfSocial);
    }
}
