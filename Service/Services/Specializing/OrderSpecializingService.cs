using Entities;
using Entities.Configuration;
using Extensions;
using Google.Apis.Auth;
using Interface.DbContext;
using Interface.Services;
using Interface.Services.Configuration;
using Interface.Services.Specializing;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Models;
using Request.Auth;
using Request.RequestCreate;
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
    public class OrderSpecializingService : IOrderSpecializingService
    {
        private IAppDbContext coreDbContext;
        private IConfiguration configuration;
        private IOrderService orderService;

        private IUserService userService;
        private IOTPHistoryService oTPHistoryService;
        private IOTPHistoriesSpecializingService oTPHistoriesSpecializingService;
        private IHangFireManageSpecializingService hangFireManageSpecializingService;
        private IEmailConfigurationService emailConfigurationService;
        private INotificationService notificationService;

        public OrderSpecializingService(
            IConfiguration configuration
            , IAppDbContext coreDbContext
            , IUserService userService
            , IOrderService orderService
            , IOTPHistoriesSpecializingService oTPHistoriesSpecializingService
            , IOTPHistoryService oTPHistoryService
            , IHangFireManageSpecializingService hangFireManageSpecializingService
            , IEmailConfigurationService emailConfigurationService
            , INotificationService notificationService
            ) {

            this.configuration = configuration;
            this.coreDbContext = coreDbContext;
            this.userService = userService;
            this.oTPHistoryService = oTPHistoryService;
            this.oTPHistoriesSpecializingService = oTPHistoriesSpecializingService;
            this.hangFireManageSpecializingService = hangFireManageSpecializingService;
            this.emailConfigurationService = emailConfigurationService;

            this.orderService = orderService;
            // phương thức thanh toán
            this.notificationService = notificationService;
        }

        
    }
}
