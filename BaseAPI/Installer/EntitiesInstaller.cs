using Entities.Configuration;
using Interface.Services;
using Interface.Services.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using Service.Services.Configurations;
//using SignalrHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseAPI.Installer
{
    public class EntitiesInstaller : IInstaller
    {
        public void Installer(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddScoped<IEmailConfigurationService, EmailConfigurationService>();
            services.AddTransient<IOTPHistoryService, OTPHistoryService>();
            services.AddTransient<IHangFireManageService, HangFireManageService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IUserNotificationService, UserNotificationService>();
            services.AddTransient<IProfileGroupService, ProfileGroupService>();
            services.AddTransient<IStudentProfileService, StudentProfileService>();
            services.AddTransient<IExaminationRoomService, ExaminationRoomService>();
            services.AddTransient<IStudentExamService, StudentExamService>();

            #region paypal
            //services.AddTransient<IPlanPaypalService, PlanPaypalService>();
            #endregion

            #region stripe
            //services.AddTransient<IPriceStripeService, PriceStripeService>();
            #endregion

        }
    }
}
