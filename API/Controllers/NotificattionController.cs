using Entities;
using Entities.Search;
using Extensions;
using Hangfire;
using Interface.Services;
using Interface.Services.Configuration;
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
    /// Quản lý Thông báo
    /// </summary>
    [Route("api/notification")]
    [ApiController]
    [Description("Quản lý thông báo")]
    [Authorize]
    public class NotificattionController : BaseController<Notification, NotificationModel, NotificationCreate, NotificationUpdate, NotificationSearch>
    {
        IUserNotificationService userNotificationService;
        public NotificattionController(IServiceProvider serviceProvider, ILogger<BaseController<Notification, NotificationModel, NotificationCreate, NotificationUpdate, NotificationSearch>> logger
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<INotificationService>();
            this.userNotificationService = serviceProvider.GetRequiredService<IUserNotificationService>();
        }
        public async override Task<AppDomainResult> AddItem([FromBody] NotificationCreate itemModel)
        {
                var jobId = BackgroundJob.Schedule(
                            () => userNotificationService.addUserNotification(itemModel),
                            DateTime.Now);
                return await base.AddItem(itemModel);

            throw new Exception("Lỗi hệ thống");
        }
    }
    
}