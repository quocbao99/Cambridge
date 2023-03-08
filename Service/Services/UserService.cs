using Entities;
using Extensions;
using Interface.DbContext;
using Interface.Services;
using Interface.UnitOfWork;
using Utilities;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.ExpressionGraph;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Service.Services.DomainServices;
using Entities.Search;
using Newtonsoft.Json;
using System.Reflection;

namespace Service.Services
{
    public class UserService : DomainService<Users, UserSearch>, IUserService
    {
        protected IAppDbContext coreDbContext;
        public UserService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
        }
        protected override string GetStoreProcName()
        {
            return "User_GetPagingUser";
        }

        /// <summary>
        /// Kiểm tra user đã tồn tại chưa?
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override async Task<string> GetExistItemMessage(Users item)
        {
            List<string> messages = new List<string>();
            string result = string.Empty;
            bool isExistEmail = !string.IsNullOrEmpty(item.Email) && await Queryable.AnyAsync(x => x.Deleted == false && x.Id != item.Id && x.Email == item.Email && x.IsVerification == true);
            bool isExistPhone = !string.IsNullOrEmpty(item.Phone) && await Queryable.AnyAsync(x => x.Deleted == false && x.Id != item.Id && x.Phone == item.Phone && x.IsVerification == true);
            bool isExistUserName = !string.IsNullOrEmpty(item.Username)
                && await Queryable.AnyAsync(x => x.Deleted == false && x.Id != item.Id && x.IsVerification == true
                && (x.Username == item.Username
                || x.Email == item.Username
                || x.Phone == item.Username
                ));
            bool isPhone = ValidateUserName.IsPhoneNumber(item.Username);
            bool isEmail = ValidateUserName.IsEmail(item.Username);


            if (isExistEmail)
                messages.Add("Email đã tồn tại!");
            if (isExistPhone)
                messages.Add("Số điện thoại đã tồn tại!");
            if (isExistUserName)
            {
                if (isPhone)
                    messages.Add("Số điện thoại đã tồn tại!");
                else if (isEmail)
                    messages.Add("Email đã tồn tại!");
                else
                    messages.Add("User name đã tồn tại!");
            }
            if (messages.Any())
                result = string.Join(" ", messages);
            return result;
        }

        /// <summary>
        /// Cập nhật password mới cho user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserPassword(Guid userId, string newPassword)
        {
            bool result = false;

            var existUserInfo = await this.unitOfWork.Repository<Users>().GetQueryable().Where(e => e.Id == userId).FirstOrDefaultAsync();
            if (existUserInfo != null)
            {
                existUserInfo.Password = newPassword;
                existUserInfo.Updated = Timestamp.Now();
                Expression<Func<Users, object>>[] includeProperties = new Expression<Func<Users, object>>[]
                {
                    e => e.Password,
                    e => e.Updated
                };
                await this.unitOfWork.Repository<Users>().UpdateFieldsSaveAsync(existUserInfo, includeProperties);
                await this.unitOfWork.SaveAsync();
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Kiểm tra user đăng nhập
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        //public async Task<bool> Verify(string userName, string password)
        //{

        //    var user = await Queryable
        //        .Where(e => e.Deleted == false
        //        && (e.Username == userName
        //        || e.Phone == userName
        //        || e.Email == userName
        //        )
        //        )
        //        .FirstOrDefaultAsync();
        //    if (user != null)
        //    {
        //        if (user.Active == false)
        //        {
        //            throw new Exception("Tài khoản chưa được kích hoạt");
        //        }
        //        if (user.Password == SecurityUtilities.HashSHA1(password))
        //        {
        //            return true;
        //        }
        //        else
        //            return false;

        //    }
        //    else
        //        return false;
        //}

        /// <summary>
        /// Kiểm tra user đăng nhập
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Users> Login(string userName, string password)
        {

            Users user = await Queryable.Where(e => e.Deleted == false && e.Active == true && e.IsVerification == true && (e.Username == userName || e.Phone == userName || e.Email == userName)).FirstOrDefaultAsync();
            if (user != null)
            {
                if (user.Active == false || user.IsVerification == false)
                {
                    throw new Exception("Tài khoản chưa được kích hoạt");
                }
                var a = SecurityUtilities.HashSHA1(password);
                if (user.Password == SecurityUtilities.HashSHA1(password))
                {
                    return user;
                }
            }
            return null;
        }

        public async Task<Users> LoginOtp(string userName, string password)
        {

            Users user = await Queryable.Where(e => e.Deleted == false && (e.Username == userName || e.Phone == userName || e.Email == userName)).OrderByDescending(d=>d.Created).FirstOrDefaultAsync();
            if (user != null)
            {
                var a = SecurityUtilities.HashSHA1(password);
                if (user.Password == SecurityUtilities.HashSHA1(password))
                {
                    return user;
                }
            }
            return null;
        }

        public async Task<Users> LoginComfirmMail (string userName )
        {
            Users user = await Queryable.Where(e => e.Deleted == false && (e.Username == userName || e.Phone == userName || e.Email == userName)).OrderByDescending(d => d.Created).FirstOrDefaultAsync();
            if (user != null)
            {
                return user;
            }
            return null;
        }

        /// <summary>
        /// Kiểm tra pass word cũ đã giống chưa
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> CheckCurrentUserPassword(Guid userId, string password, string newPasssword)
        {
            string message = string.Empty;
            List<string> messages = new List<string>();
            bool isCurrentPassword = await this.Queryable.AnyAsync(x => x.Id == userId && x.Password == SecurityUtilities.HashSHA1(password));
            bool isDuplicateNewPassword = await this.Queryable.AnyAsync(x => x.Id == userId && x.Password == SecurityUtilities.HashSHA1(newPasssword));
            if (!isCurrentPassword)
                messages.Add("Mật khẩu cũ không chính xác");
            else if (isDuplicateNewPassword)
                messages.Add("Mật khẩu mới không được trùng mật khẩu cũ");
            if (messages.Any())
                message = string.Join("; ", messages);
            return message;
        }

        /// <summary>
        /// Cập nhật thông tin user token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="isLogin"></param>
        /// <returns></returns>
        //public async Task<bool> UpdateUserToken(Guid userId, string token, bool isLogin = false)
        //{
        //    bool result = false;

        //    var userInfo = await this.unitOfWork.Repository<Users>().GetQueryable().Where(e => e.Id == userId).FirstOrDefaultAsync();
        //    if (userInfo != null)
        //    {
        //        if (isLogin)
        //        {
        //            userInfo.Token = token;
        //            userInfo.ExpiredDate = Timestamp.Date(DateTime.UtcNow.AddDays(1));
        //        }
        //        else
        //        {
        //            userInfo.Token = string.Empty;
        //            userInfo.ExpiredDate = null;
        //        }
        //        Expression<Func<Users, object>>[] includeProperties = new Expression<Func<Users, object>>[]
        //        {
        //            e => e.Token,
        //            e => e.ExpiredDate
        //        };
        //        this.unitOfWork.Repository<Users>().UpdateFieldsSave(userInfo, includeProperties);
        //        await this.unitOfWork.SaveAsync();
        //        result = true;
        //    }

        //    return result;
        //}
        public async Task<bool> HasPermission(string permissionArray, string controller, string action)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(permissionArray))
                    return false;
                string[] permission = permissionArray.Split('|');
                foreach (var item in permission)
                {
                    List<Permission> permissions = JsonConvert.DeserializeObject<List<Permission>>(item);
                    if (permissions.Any(e => e.Controller == controller && e.PermissionActions.Any(x => x.Action == action && x.Allowed == true)))
                        return true;
                }
                return false;
            });
        }

        public async Task<bool> ExpiredTrial(Guid userID)
        {
            var user = await this.GetByIdAsync(userID);
            if (user == null) throw new Exception("Lỗi hệ thống");
            user.IsTrial = false;
            return await this.UpdateFieldAsync(user, d => d.IsTrial);
        }
    }
}
