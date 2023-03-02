using Interface.Services;
using Interface.Services.Specializing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using Service.Services.Specializing;
//using SignalrHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseAPI.Installer
{
    public class ServicesInstaller : IInstaller
    {
        public void Installer(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient<IAddMarterialService, AddMarterialService>();
            //services.AddTransient<IAddMarterialSubService, AddMarterialSubService>();
            //services.AddTransient<IUserSpecializingService, UserSpecializingService>();
            //services.AddTransient<IHangFireManageSpecializingService, HangFireManageSpecializingService>();
            //services.AddTransient<IOTPHistoriesSpecializingService, OTPHistoriesSpecializingService>();
            //services.AddTransient<IOrderSpecializingService, OrderSpecializingService>();
            //services.AddTransient<IContractSpecializingService, ContractSpecializingService>();
            //services.AddTransient<ICurrencyExchangeRateService, CurrencyExchangeRateService>();
            //services.AddTransient<IGetStatisticalService, GetStatisticalService>();
            services.AddTransient<IOneSignalService, OneSignalService>();
        }
    }
}
