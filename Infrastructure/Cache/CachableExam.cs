using Application.Common.Interfaces;
using Application.Models.Exam;
using Application.Models.ExamResult;
using Application.Models.Pagination;
using Infrastructure.Cache.CacheOptions;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Cache
{
    public class CachableExam : IExamService
    {
        private readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
        };
        private readonly IExamService _parentExamService;
        private readonly DistributedCacheEntryOptions _cacheEntryOptions;
        private readonly CachableExamOption _options;
        private readonly IDistributedCache _cache;
        public CachableExam( IExamService parentExamService, IDistributedCache cache, CachableExamOption options)
        {
            _parentExamService = parentExamService ?? throw new ArgumentNullException(nameof(parentExamService));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _options = options;
            _cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _options.CacheTimeSpan
            };
        }
        public async Task<ExamDetailResponse> Create(CreateExamRequest request)
        {
            var result = await _parentExamService.Create(request);
            var cacheKey = $"{_options.CacheKey}{result.Id}";
            var cacheData = JsonSerializer.Serialize(result, serializerOptions);
            await _cache.SetStringAsync(cacheKey, cacheData, _cacheEntryOptions);
            await _cache.RemoveAsync($"{_options.CacheKey}category:{result.ExamCategoryId}");
            return result;
        }

        public async Task<bool> Delete(Guid id)
        {
            var exam = await _parentExamService.GetById(id);

            var result = await _parentExamService.Delete(id);

            if (result)
            {
                await _cache.RemoveAsync($"{_options.CacheKey}{id}");
                await _cache.RemoveAsync($"{_options.CacheKey}take:{id}");
                await _cache.RemoveAsync($"{_options.CacheKey}category:{exam.ExamCategoryId}");
            }

            return result;
        }

        public async Task<ExamResponse> GetById(Guid id)
        {
            var cacheKey = $"{_options.CacheKey}{id}";
            var cacheData = await _cache.GetStringAsync(cacheKey);
            if(cacheData != null)
            {
                var result = JsonSerializer.Deserialize<ExamResponse>(cacheData, serializerOptions);
                if(result != null)
                    return result;
            }
            var exam = await _parentExamService.GetById(id);
            cacheData = JsonSerializer.Serialize(exam, serializerOptions);
            await _cache.SetStringAsync(cacheKey, cacheData, _cacheEntryOptions);
            return exam;
        }

        public async Task<IEnumerable<ExamResponse>?> GetExamsByCategoryIdAsync(Guid id)
        {
            var cacheKey = $"{_options.CacheKey}category:{id}";
            var cacheData = await _cache.GetStringAsync(cacheKey);
            if (cacheData != null)
            {
                var result = JsonSerializer.Deserialize<IEnumerable<ExamResponse>>(cacheData, serializerOptions);
                if (result != null)
                    return result;
            }
            var exam = await _parentExamService.GetExamsByCategoryIdAsync(id);
            cacheData = JsonSerializer.Serialize(exam, serializerOptions);
            await _cache.SetStringAsync(cacheKey, cacheData, _cacheEntryOptions);
            return exam;
        }

        public async Task<TakeExamResponse> GetExamToTake(Guid id)
        {
            var cacheKey = $"{_options.CacheKey}take:{id}";
            var cacheData = await _cache.GetStringAsync(cacheKey);
            if (cacheData != null)
            {
                var result = JsonSerializer.Deserialize<TakeExamResponse>(cacheData, serializerOptions);
                if (result != null)
                    return result;
            }
            var exam = await _parentExamService.GetExamToTake(id);
            cacheData = JsonSerializer.Serialize(exam, serializerOptions);
            await _cache.SetStringAsync(cacheKey, cacheData, _cacheEntryOptions);
            return exam;
        }

        public async Task<PaginationResponse<ExamResponse>> GetPaginated(PaginatedRequest request)
        {
            return await _parentExamService.GetPaginated(request);
        }

        public async Task<TakeExamResponse> GetRandomExamToTake()
        {
            return await _parentExamService.GetRandomExamToTake();
        }

        public async Task<bool> SoftDelete(Guid id)
        {
            var exam = await _parentExamService.GetById(id);

            var result = await _parentExamService.SoftDelete(id);

            if (result)
            {
                await _cache.RemoveAsync($"{_options.CacheKey}{id}");
                await _cache.RemoveAsync($"{_options.CacheKey}take:{id}");
                await _cache.RemoveAsync($"{_options.CacheKey}category:{exam.ExamCategoryId}");
            }

            return result;
        }

        public async Task<ExamResultDetailResponse> SubmitExam(Guid userId, SubmitExamRequest request)
        {
            return await _parentExamService.SubmitExam(userId, request);
        }

        public async Task<ExamDetailResponse> Update(UpdateExamRequest request)
        {
            var result = await _parentExamService.Update(request);
            var cacheKey = $"{_options.CacheKey}{request.Id}";
            var cacheData = JsonSerializer.Serialize(result, serializerOptions);
            await _cache.SetStringAsync(cacheKey, cacheData, _cacheEntryOptions);
            await _cache.RemoveAsync($"{_options.CacheKey}category:{result.ExamCategoryId}");
            return result;
        }
    }
}
