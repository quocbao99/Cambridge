using Interface.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;
//using SignalrHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseAPI.Installer
{
    public class PaymentMethodInstaller : IInstaller
    {
        public void Installer(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient<IPaymentByMomoService, PaymentByMomoService>();
            //services.AddTransient<IPaymentByPaypalService, PaymentByPaypalService>();
            //services.AddTransient<IPaymentByVnPayService, PaymentByVnPayService>();
            //services.AddTransient<IPaymentByStripeService, PaymentByStripeService>();
        }
    }
}
