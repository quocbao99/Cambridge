using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseAPI.Installer
{
    public interface IInstaller
    {
        void Installer(IServiceCollection services, IConfiguration configuration);
    }
}
