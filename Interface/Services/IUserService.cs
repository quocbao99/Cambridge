using Entities;
using Entities.Search;
using Interface.Services.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Services
{
    public interface IUserService: IDomainService<Users, UserSearch>
    {
        //Task<bool> Verify(string userName, string password);
        Task<Users> Login(string userName, string password);
        Task<Users> LoginOtp(string userName, string password);
        Task<string> CheckCurrentUserPassword(Guid userId, string password, string newPasssword);
        //Task<bool> UpdateUserToken(Guid userId, string token, bool isLogin = false);
        Task<bool> UpdateUserPassword(Guid userId, string newPassword);
        Task<bool> HasPermission(string roles, string controller, string permission);
        Task<bool> ExpiredTrial(Guid userID);
        Task<Users> LoginComfirmMail(string userName);

    }
}
