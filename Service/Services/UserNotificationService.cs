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
using Interface.Services.Configuration;
using Request.RequestCreate;
using static Utilities.CoreContants;
using Hangfire;

namespace Service.Services
{
    public class UserNotificationService : DomainService<UserNotification, UserNotificationSearch>, IUserNotificationService
    {
        protected IAppDbContext coreDbContext;
        IUserService userService;
        IOneSignalService oneSignalService;
        IEmailConfigurationService emailConfigurationService;
        public UserNotificationService(IAppUnitOfWork unitOfWork, IMapper mapper, IAppDbContext coreDbContext
            , IUserService userService,
            IOneSignalService oneSignalService,
            IEmailConfigurationService emailConfigurationService
            ) : base(unitOfWork, mapper)
        {
            this.coreDbContext = coreDbContext;
            this.userService = userService;
            this.oneSignalService = oneSignalService;
            this.emailConfigurationService = emailConfigurationService;
        }
        protected override string GetStoreProcName()
        {
            return "UserNotification_GetPagingUserNotification";
        }

        public async Task<bool> addUserNotification(NotificationCreate itemModel) {
            if (NotificationType.USER.GetHashCode().ToString().Contains(itemModel.Type))
            {
                if (itemModel.UserID is null) throw new Exception("thông tin User không được để trống");
                var user = await userService.GetByIdAsync((Guid)itemModel.UserID);
                if (user is null) throw new Exception("không tìm thấy thông tin User");
                
                if (itemModel.IsSendMail == true)
                {
                    await emailConfigurationService.SendMail(itemModel.Title, user.Email, new string[] { "" }, new string[] { "" }, new EmailContent { isHtml = true, content = itemModel.Content, attachments = null });
                }
                await oneSignalService.CreateOneSignal(itemModel.Title, itemModel.Content, new string[] { user.OneSignalID });
                var userNorification = new UserNotification();
                userNorification.Created = Timestamp.Now();
                userNorification.Title = itemModel.Title;
                userNorification.Title = itemModel.Title;
                userNorification.Content = itemModel.Content;
                userNorification.IsSendMail= itemModel.IsSendMail;
                userNorification.IsRead= false;
                userNorification.LinkApp= itemModel.LinkApp;
                userNorification.LinkWeb = itemModel.LinkWeb;
                userNorification.Type= itemModel.Type;
                userNorification.UserID= user.Id;
                return await this.CreateAsync(userNorification);

            }
            else if (NotificationType.USERs.GetHashCode().ToString().Contains(itemModel.Type))
            {
                var userids = itemModel.UserIDs.Split(",");
                if (userids is null) throw new Exception("không tìm thấy thông tin User");
                string[] oneSignalIDs = new string[] { }; ;
                foreach (var userid in userids)
                {
                    var user = await userService.GetByIdAsync(new Guid(userid));
                    oneSignalIDs.Append(user.OneSignalID);
                    var userNorification = new UserNotification();
                    if (itemModel.IsSendMail == true)
                    {
                        await emailConfigurationService.SendMail(itemModel.Title, user.Email, new string[] { "" }, new string[] { "" }, new EmailContent { isHtml = true, content = itemModel.Content, attachments = null });
                    }
                    var userNorification2 = new UserNotification();
                    userNorification2.Created = Timestamp.Now();
                    userNorification2.Title = itemModel.Title;
                    userNorification2.Content = itemModel.Content;
                    userNorification2.IsSendMail = itemModel.IsSendMail;
                    userNorification2.IsRead = false;
                    userNorification2.LinkApp = itemModel.LinkApp;
                    userNorification2.LinkWeb = itemModel.LinkWeb;
                    userNorification2.Type = itemModel.Type;
                    userNorification2.UserID = user.Id;
                    await this.CreateAsync(userNorification2);
                }
                await oneSignalService.CreateOneSignal(itemModel.Title, itemModel.Content, oneSignalIDs);
                return true;
                //return new AppDomainResult() { ResultCode =(int)HttpStatusCode.OK, Success= true, ResultMessage= "thành công" };
            }
            else if (NotificationType.ROLES.GetHashCode().ToString().Contains(itemModel.Type))
            {
                var roleids = itemModel.Roles.Split(",");
                if (roleids is null) throw new Exception("không tìm thấy thông tin phân quyền");

                var users = await userService.GetAsync(d => d.Roles == itemModel.Roles && d.Active == true && d.Deleted == false && d.IsVerification == true);
                if (users is null) throw new Exception("không tìm thấy thông tin User");
                string[] oneSignalIDs = new string[] { };
                foreach (var user in users)
                {
                    oneSignalIDs.Append(user.OneSignalID);
                    if (itemModel.IsSendMail == true)
                    {
                        var sendmail = BackgroundJob.Schedule(
                             () =>  emailConfigurationService.SendMail(itemModel.Title, user.Email, new string[] { "" }, new string[] { "" }, new EmailContent { isHtml = true, content = itemModel.Content, attachments = null }),
                            DateTime.Now);
                        //await emailConfigurationService.SendMail(itemModel.Title, user.Email, new string[] { "" }, new string[] { "" }, new EmailContent { isHtml = true, content = itemModel.Content, attachments = null });
                    }
                    var userNorification3 = new UserNotification();
                    userNorification3.Created = Timestamp.Now();
                    userNorification3.Title = itemModel.Title;
                    userNorification3.Content = itemModel.Content;
                    userNorification3.IsSendMail = itemModel.IsSendMail;
                    userNorification3.IsRead = false;
                    userNorification3.LinkApp = itemModel.LinkApp;
                    userNorification3.LinkWeb = itemModel.LinkWeb;
                    userNorification3.Type = itemModel.Type;
                    userNorification3.UserID = user.Id;
                    await this.CreateAsync(userNorification3);
                }
                var sendOneSignal = BackgroundJob.Schedule(
                             () => oneSignalService.CreateOneSignal(itemModel.Title, itemModel.Content, oneSignalIDs), DateTime.Now);
                //await oneSignalService.CreateOneSignal(itemModel.Title, itemModel.Content, oneSignalIDs);
                return true;
                //return new AppDomainResult() { ResultCode = (int)HttpStatusCode.OK, Success = true, ResultMessage = "thành công" };
            }
            throw new Exception("Lỗi hệ thống");
        }
    }
}
