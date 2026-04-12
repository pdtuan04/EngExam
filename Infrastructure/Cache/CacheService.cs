using Application.Abstractions.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Cache
{
    public sealed class CacheService : ICacheService
    {
        private readonly static TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);
        private readonly IDistributedCache _cache;
        private readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
        };
        private readonly DistributedCacheEntryOptions _cacheEntryOptions;
        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
            _cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = DefaultExpiration,
            };
        }

        public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<CancellationToken, Task<T>> factory, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            var cacheResult = await _cache.GetStringAsync(cacheKey, cancellationToken);
            if(cacheResult != null)
            {
                T result = JsonSerializer.Deserialize<T>(cacheResult, serializerOptions);
                return result;
            }
            var value = await factory(cancellationToken);
            var options = expiration.HasValue ? new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration.Value,
            } : _cacheEntryOptions;
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(value, serializerOptions), options, cancellationToken);
            return value;
        }
    }
}
