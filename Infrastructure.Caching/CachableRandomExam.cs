using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTOs.Responses;
using Application.Exceptions;
using Application.Interface;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Caching
{
    public class CachableRandomExam : IGetRandomExam
    {
        private static readonly JsonSerializerOptions serializerOptions = new()
        {

        };
        private readonly ILogger<CachableRandomExam> _logger;
        private readonly IGetRandomExam _getRandomExam;
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _cacheEntryOptions;
        private readonly CachableRandomExamOptions options;
        public CachableRandomExam(ILogger<CachableRandomExam> logger, IGetRandomExam getRandomExam, IDistributedCache cache, CachableRandomExamOptions options)
        {
            this._logger = logger;
            this._getRandomExam = getRandomExam ?? throw new ArgumentNullException(nameof(getRandomExam)); ;
            this._cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this.options = options;
            _cacheEntryOptions = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = options.CacheTimeSpan
            };
        }

        public async Task<ExamResponse> GetRandomExamAsync()
        {
            var cacheKey = $"{options.CacheKey}";
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (cachedData == null)
            {
                _logger.LogInformation("Loading exam from parent...");
                var exam = await _getRandomExam.GetRandomExamAsync() ?? throw new ExamNullException();
                cachedData = JsonSerializer.Serialize(exam, serializerOptions);
                _logger.LogInformation("Storing {data} to cache...", cachedData);
                await _cache.SetStringAsync(cacheKey, cachedData, _cacheEntryOptions);
                return exam;
            }else
            {
                var exam = JsonSerializer.Deserialize<ExamResponse>(cachedData);
                return exam;
            }
        }
    }
}
