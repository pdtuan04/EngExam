using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTOs.Responses;
using Application.Exceptions;
using Application.Interface;
using Application.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Application.Caching
{
    public class CachableExamById : IGetExamFinder
    {
        private readonly IGetExamFinder _getExam;
        private readonly IDistributedCache _cache;
        private readonly CachableExamByIdOptions _options;
        private ILogger<CachableExamById> _logger;
        private DistributedCacheEntryOptions _cacheEntryOptions;
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
        };

        public CachableExamById(
            IGetExamFinder getExam,
            IDistributedCache cache,
            CachableExamByIdOptions options,
            ILogger<CachableExamById> logger)
        {
            _getExam = getExam ?? throw new ArgumentNullException(nameof(getExam));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = options.Expiration
            };
        }
        public async Task<ExamResponse> GetExamByIdAsync(Guid id)
        {
            var cacheKey = $"{_options.CacheKey}#{id}";
            string? cachedData = null;
            try
            {
                cachedData = await _cache.GetStringAsync(cacheKey);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error accessing the cache.");
            }
            if (cachedData == null)
            {
                _logger.LogInformation($"Cache miss for key: {cacheKey}");
                var exam = await _getExam.GetExamByIdAsync(id) ?? throw new ExamNullException();
                try
                {
                    cachedData = JsonSerializer.Serialize(exam, _serializerOptions);
                    await _cache.SetStringAsync(cacheKey, cachedData, _cacheEntryOptions);
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Error setting the cache.");
                }
                return exam;
            }
            else
            {
                _logger.LogInformation($"Cache hit for key: {cacheKey}");

                var exam = JsonSerializer.Deserialize<ExamResponse>(cachedData, _serializerOptions);
                if (exam == null)
                {
                    _logger.LogWarning($"Deserialization returned null for key: {cacheKey}.  Invalidating cache.");
                    await _cache.RemoveAsync(cacheKey);
                    return await GetExamByIdAsync(id);
                }
                return exam;
            }
        }
    }
}
