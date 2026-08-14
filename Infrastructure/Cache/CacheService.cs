using Application.Abstractions.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
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
        private readonly ILogger<CacheService> _logger;
        private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;
        public CacheService(IDistributedCache cache, ILogger<CacheService> logger)
        {
            _cache = cache;
            _logger = logger;
            _circuitBreakerPolicy = Policy
                .Handle<Exception>()
                .CircuitBreakerAsync(
                    exceptionsAllowedBeforeBreaking: 3,
                    durationOfBreak: TimeSpan.FromMinutes(5),
                    onBreak: (exception, breakDelay) =>
                    {
                        _logger.LogWarning(exception, "Cache service is in a broken state for {BreakDelay} due to an exception.", breakDelay);
                    },
                    onReset: () =>
                    {
                        _logger.LogInformation("Cache service has been reset and is now operational.");
                    },
                    onHalfOpen: () =>
                    {
                        _logger.LogInformation("Cache service is in a half-open state and will test the next operation.");
                    }
                );
            _cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = DefaultExpiration,
            };
        }

        public async Task<T?> GetAsync<T>(string cacheKey)
        {
            try
            {
                var cacheResult = await _cache.GetStringAsync(cacheKey);
                if (cacheResult != null)
                {
                    T result = JsonSerializer.Deserialize<T>(cacheResult, serializerOptions);
                    return result;
                }
            }
            catch (BrokenCircuitException)
            {
                _logger.LogWarning("Circuit breaker is open");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching cache for key: {CacheKey}", cacheKey);
            }
            return default(T);
        }

        public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<CancellationToken, Task<T>> factory, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var cacheResult = await _cache.GetStringAsync(cacheKey, cancellationToken);
                if (cacheResult != null)
                {
                    T result = JsonSerializer.Deserialize<T>(cacheResult, serializerOptions);
                    return result;
                }
            }
            catch (BrokenCircuitException)
            {
                _logger.LogWarning("Circuit breaker is open");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching cache for key: {CacheKey}", cacheKey);
            }
            var value = await factory(cancellationToken);
            try
            {
                var options = expiration.HasValue ? new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiration.Value,
                } : _cacheEntryOptions;
                await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(value, serializerOptions), options, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while setting cache for key: {CacheKey}", cacheKey);
            }
            return value;
        }

        public async Task RemoveCacheAsync(string cacheKey, CancellationToken cancellationToken = default)
        {
            try
            {
                await _cache.RemoveAsync(cacheKey, cancellationToken);
            }
            catch (BrokenCircuitException)
            {
                _logger.LogWarning("Circuit breaker is open");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error occurred while removing cache for key: {CacheKey}", cacheKey);
            }
        }

        public async Task SetAsync<T>(string cacheKey, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var options = expiration.HasValue ? new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiration.Value,
                } : _cacheEntryOptions;
                await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(value, serializerOptions), options, cancellationToken);
            }
            catch (BrokenCircuitException)
            {
                _logger.LogWarning("Circuit breaker is open");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error occurred while setting cache for key: {CacheKey}", cacheKey);
            }
        }
    }
}