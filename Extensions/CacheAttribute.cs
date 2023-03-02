using Interface.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using Request.RequestCreate;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Extensions
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;
        public CacheAttribute(int timeToLiveSeconds = 432000) // 5 Ngày
        {
            _timeToLiveSeconds = timeToLiveSeconds;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            throw new NotImplementedException();
        }
        //public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    RedisConfiguration redisConfiguration = context.HttpContext.RequestServices.GetRequiredService<RedisConfiguration>();
        //    if (!redisConfiguration.Enabled)
        //    {
        //        //nếu không cấu hình sử dụng cache => cho chạy middware tiếp theo (ở đây là chạy vô action để lấy data xong rồi return luôn)
        //        await next();
        //        return;
        //    }
        //    RequestBookCreateModel data = (RequestBookCreateModel)context.ActionArguments.Values.FirstOrDefault();

        //    string cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request, data);
        //    var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();


        //    var multiplexers = new List<RedLockMultiplexer>
        //    {
        //        ConnectionMultiplexer.Connect(redisConfiguration.ConnectionString),
        //    };
        //    var redlockFactory = RedLockFactory.Create(multiplexers);
        //    await using (var redLock = await redlockFactory.CreateLockAsync(cacheKey, TimeSpan.FromSeconds(30)))
        //    {
        //        if (redLock.IsAcquired)
        //        {
        //            DataCacheBookingTour cacheResponse =await cacheService.GetBookingTicketCachedResponseAsync(cacheKey);

        //            if (cacheResponse != null)
        //            {
        //                var numTicket = data.Customers.Count();
        //                if (cacheResponse.total - cacheResponse.success - cacheResponse.waiting - numTicket < 0)
        //                {
        //                    context.Result = new ContentResult
        //                    {
        //                        Content = new AppDomainResult()
        //                        {
        //                            ResultCode = (int)HttpStatusCode.OK,
        //                            ResultMessage = "thành công!",
        //                            Data = "Hết vé rồi bà con ơi",
        //                            Success = true
        //                        }.ToString(),
        //                        ContentType = "application/json",
        //                        StatusCode = 200
        //                    };
        //                    return;
        //                }
        //                else
        //                {
        //                    // nếu còn đủ vé thì tăng cache lên
        //                    cacheResponse.waiting = cacheResponse.waiting + numTicket;
        //                    await cacheService.UpdateBookingTicketCachedResponseAsync(cacheKey, cacheResponse, TimeSpan.FromSeconds(_timeToLiveSeconds));
        //                }
        //            }
        //            var excutedContext = await next();

        //            //nếu không có cache => lấy data => gán vô cache
        //            if (excutedContext.Result is OkObjectResult objectResult)
        //            {
        //                await cacheService.SetCacheResponseAsync(cacheKey, objectResult.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
        //            }
        //        }
        //    }
        //}
        ///// <summary>
        ///// Tạo CacheKey theo request
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //private static string GenerateCacheKeyFromRequest(HttpRequest request, RequestBookCreateModel obj)
        //{
        //    StringBuilder keyBuilder = new StringBuilder();
        //    keyBuilder.Append($"{request.Path}");
        //    foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
        //    {
        //        keyBuilder.Append($"|{key}-{value}");
        //    }
        //    keyBuilder.Append(obj.TripID);
        //    return keyBuilder.ToString();
        //}
    }
}
