using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTOs.Exam.Doing;
using Application.DTOs.Responses;
using Application.Exceptions;
using Application.Interface.Exam;
using Application.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Application.Caching
{
    public class CachableExamFinder : IGetExamFinder
    {
        private readonly IGetExamFinder _getExam;
        private readonly IDistributedCache _cache;
        private readonly CachableExamFinderOptions _options;
        private ILogger<CachableExamFinder> _logger;
        private DistributedCacheEntryOptions _cacheEntryOptions;
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
        };

        public CachableExamFinder(
            IGetExamFinder getExam,
            IDistributedCache cache,
            CachableExamFinderOptions options,
            ILogger<CachableExamFinder> logger)
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
        public async Task<ExamForDoingDTO> GetExamForDoingAsync(Guid id)
        {
            var cacheKey = $"{_options.CacheKey}{id}";
            string? cachedData = null;
            try
            {
                cachedData = await _cache.GetStringAsync(cacheKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error accessing the cache.");
            }
            if (string.IsNullOrEmpty(cachedData))
            {
                _logger.LogInformation($"Cache miss for key: {cacheKey}");
                var exam = await _getExam.GetExamForDoingAsync(id) ?? throw new ExamNullException();
                try
                {
                    cachedData = JsonSerializer.Serialize(exam, _serializerOptions);
                    await _cache.SetStringAsync(cacheKey, cachedData, _cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error setting the cache.");
                }
                return exam;
            }
            else
            {
                _logger.LogInformation($"Cache hit for key: {cacheKey}");

                var exam = JsonSerializer.Deserialize<ExamForDoingDTO>(cachedData, _serializerOptions);
                if (exam == null)
                {
                    _logger.LogWarning($"Deserialization returned null for key: {cacheKey}.  Invalidating cache.");
                    await _cache.RemoveAsync(cacheKey);
                    return await GetExamForDoingAsync(id);
                }
                return exam;
            }
        }

        public async Task<IEnumerable<ExamSummaryDto>> GetExamsByCategoryIdAsync(Guid id)
        {
            var cacheKey = $"{_options.CacheKey}Category|{id}";
            string? cachedData = null;
            try
            {
                cachedData = await _cache.GetStringAsync(cacheKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error accessing the cache.");
            }
            if(string.IsNullOrEmpty(cachedData))
            {
                _logger.LogInformation($"Cache miss for key: {cacheKey}");
                var exams = await _getExam.GetExamsByCategoryIdAsync(id) ?? throw new Exception();
                try
                {
                    cachedData = JsonSerializer.Serialize(exams, _serializerOptions);
                    await _cache.SetStringAsync(cacheKey, cachedData, _cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error setting the cache.");
                }
                return exams;
            }
            else
            {
                _logger.LogInformation($"Cache hit for key: {cacheKey}");
                var exams = JsonSerializer.Deserialize<IEnumerable<ExamSummaryDto>>(cachedData, _serializerOptions);
                if (exams == null)
                {
                    _logger.LogWarning($"Deserialization returned null for key: {cacheKey}.  Invalidating cache.");
                    await _cache.RemoveAsync(cacheKey);
                    return await GetExamsByCategoryIdAsync(id);
                }
                return exams;
            }
        }
    }
}
