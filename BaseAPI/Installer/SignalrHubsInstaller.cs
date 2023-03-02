using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using SignalrHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseAPI.Installer
{
    public class SignalrHubsInstaller : IInstaller
    {
        public void Installer(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<IConnectionHub, ConnectionHub>();
        }
    }
}
