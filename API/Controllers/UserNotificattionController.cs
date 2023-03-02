using Entities;
using Entities.Search;
using Extensions;
using Interface.Services;
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
    /// Quản lý Thông báo người dùng
    /// </summary>
    [Route("api/user-notification")]
    [ApiController]
    [Description("Quản lý thông báo người dùng")]
    [Authorize]
    public class UserNotificattionController : BaseController<UserNotification, UserNotificationModel, UserNotificationCreate, UserNotificationUpdate, UserNotificationSearch>
    {
        IUserService userService; 
        IOneSignalService oneSignalService;
        INotificationService notificationService;
        public UserNotificattionController(IServiceProvider serviceProvider, ILogger<BaseController<UserNotification, UserNotificationModel, UserNotificationCreate, UserNotificationUpdate, UserNotificationSearch>> logger
            , IWebHostEnvironment env) : base(serviceProvider, logger, env)
        {
            this.domainService = serviceProvider.GetRequiredService<IUserNotificationService>();
            this.userService = serviceProvider.GetRequiredService<IUserService>();
            this.oneSignalService = serviceProvider.GetRequiredService<IOneSignalService>();
            this.notificationService = serviceProvider.GetRequiredService<INotificationService>();
        }
        
    }
    
}