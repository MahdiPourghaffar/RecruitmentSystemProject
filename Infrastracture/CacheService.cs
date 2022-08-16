using System;
using System.Text;
using Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastracture
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            var value = _cache.GetString(key);

            if (value is null)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(value);
        }

        public T Set<T>(
            string key,
            T value,
            TimeSpan? absoluteExpiration = null,
            TimeSpan? slidingExpiration = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpiration ?? TimeSpan.FromMinutes(60),
                SlidingExpiration = slidingExpiration
            };

            var jsonData = JsonConvert.SerializeObject(value);
            _cache.SetString(key, jsonData, options);

            return value;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
