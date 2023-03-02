using Interface.Services;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Service.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        public ResponseCacheService(IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer)
        {
            _distributedCache = distributedCache;
            _connectionMultiplexer = connectionMultiplexer;
        }
        /// <summary>
        /// Lấy cache
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var cacheResponse = await _distributedCache.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(cacheResponse) ? null : cacheResponse;
        }

        /// <summary>
        /// Xóa cache
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public async Task RemoveCacheResponseAsync(string pattern)
        {
            // gắn thêm * để lấy được all cache có dạng param
            await foreach (var key in GetKeyAsync(pattern + "*"))
            {
                await _distributedCache.RemoveAsync(key);
            }
        }

        /// <summary>
        /// Lấy tất cả cache có key chứa pattern truyền vào
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private async IAsyncEnumerable<string> GetKeyAsync(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new AggregateException("Value cannot be null or whitespace");
            foreach (var endPoint in _connectionMultiplexer.GetEndPoints())
            {
                var server = _connectionMultiplexer.GetServer(endPoint);
                foreach (var key in server.Keys(pattern: pattern))
                {
                    yield return key.ToString();
                }
            }
        }

        //private async Task<List<string>> GetKeyAsyncv2(string pattern)
        //{
        //    return await Task.Run(() =>
        //    {
        //        if (string.IsNullOrWhiteSpace(pattern))
        //            throw new AggregateException("Value cannot be null or whitespace");
        //        List<string> listKey = new List<string>();
        //        foreach (var endPoint in _connectionMultiplexer.GetEndPoints())
        //        {
        //            var server = _connectionMultiplexer.GetServer(endPoint);
        //            foreach (var key in server.Keys(pattern: pattern))
        //            {
        //                listKey.Add(key.ToString());
        //            }
        //        }
        //        return listKey;
        //    });
        //}

        /// <summary>
        /// Set cache
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="response"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task SetCacheResponseAsync(string cacheKey, object response, TimeSpan timeOut)
        {
            if (response == null)
                return;
            AppDomainResult appDomain = (AppDomainResult)response;
            var serializeResponse = JsonConvert.SerializeObject(appDomain.Data, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            await _distributedCache.SetStringAsync(cacheKey, serializeResponse, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = timeOut
            });
        }

        //public async Task<DataCacheBookingTour> GetBookingTicketCachedResponseAsync(string cacheKey)
        //{
        //    var cacheResponse = await _distributedCache.GetStringAsync(cacheKey);
        //    if (string.IsNullOrEmpty(cacheResponse))
        //        return null;
        //    DataCacheBookingTour result = System.Text.Json.JsonSerializer.Deserialize<DataCacheBookingTour>(cacheResponse);
        //    return result;
        //}

        //public async Task UpdateBookingTicketCachedResponseAsync(string cacheKey, object response, TimeSpan timeOut)
        //{
        //    if (response == null)
        //        return ;
        //    DataCacheBookingTour appDomain = (DataCacheBookingTour)response;
        //    var serializeResponse = JsonConvert.SerializeObject(appDomain, new JsonSerializerSettings()
        //    {
        //        ContractResolver = new CamelCasePropertyNamesContractResolver()
        //    });
        //    await _distributedCache.SetStringAsync(cacheKey, serializeResponse, new DistributedCacheEntryOptions()
        //    {
        //        AbsoluteExpirationRelativeToNow = timeOut
        //    });
        //}
    }
}
