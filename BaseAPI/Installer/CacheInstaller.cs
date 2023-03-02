using Interface.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace BaseAPI.Installer
{
    public class CacheInstaller : IInstaller
    {
        public void Installer(IServiceCollection services, IConfiguration configuration)
        {
            // comment redis cache
            //RedisConfiguration redisConfiguration = new RedisConfiguration();
            ////lấy cấu hình từ json gán vào
            //configuration.GetSection("RedisConfiguration").Bind(redisConfiguration);
            ////khởi tạo 1 lần, các chỗ khác dùng không cần khởi tạo lại nữa, đỡ tốn bộ nhớ
            //services.AddSingleton(redisConfiguration);

            //if (!redisConfiguration.Enabled)
            //    return;

            ////khởi tạo kết nối tới Redis
            //services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisConfiguration.ConnectionString));
            //services.AddStackExchangeRedisCache(option => option.Configuration = redisConfiguration.ConnectionString);
            //services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}
