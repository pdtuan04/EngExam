using Application.Common.Interfaces;
using Application.Models.ExamCategory;
using Application.Models.Pagination;
using Infrastructure.Cache.CacheOptions;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Cache
{
    public class CachableExamCategory : IExamCategoryService
    {
        private readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions();

        private readonly IExamCategoryService _parentService;
        private readonly IDistributedCache _cache;
        private readonly CachableExamCategoryOption _options;
        private readonly DistributedCacheEntryOptions _cacheEntryOptions;

        public CachableExamCategory(
            IExamCategoryService parentService,
            IDistributedCache cache,
            CachableExamCategoryOption options)
        {
            _parentService = parentService ?? throw new ArgumentNullException(nameof(parentService));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _options = options;

            _cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _options.CacheTimeSpan
            };
        }

        public async Task<ExamCategoryResponse> CreateExamCategogy(CreateExamCategoryRequest request)
        {
            var result = await _parentService.CreateExamCategogy(request);

            var cacheKey = $"{_options.CacheKey}category:{result.Id}";
            var cacheData = JsonSerializer.Serialize(result, serializerOptions);

            await _cache.SetStringAsync(cacheKey, cacheData, _cacheEntryOptions);

            await _cache.RemoveAsync($"{_options.CacheKey}category:all");

            return result;
        }

        public async Task DeleteExamCategogy(Guid id)
        {
            await _parentService.DeleteExamCategogy(id);
            var cacheKey = $"{_options.CacheKey}category:{id}";
            await _cache.RemoveAsync(cacheKey);
            await _cache.RemoveAsync($"{_options.CacheKey}category:all");

        }

        public async Task<ICollection<ExamCategoryResponse>> GetAll()
        {
            var cacheKey = $"{_options.CacheKey}category:all";

            var cacheData = await _cache.GetStringAsync(cacheKey);

            if (cacheData != null)
            {
                var result = JsonSerializer.Deserialize<ICollection<ExamCategoryResponse>>(cacheData, serializerOptions);

                if (result != null)
                    return result;
            }

            var categories = await _parentService.GetAll();

            cacheData = JsonSerializer.Serialize(categories, serializerOptions);

            await _cache.SetStringAsync(cacheKey, cacheData, _cacheEntryOptions);

            return categories;
        }

        public async Task<ExamCategoryResponse> GetById(Guid id)
        {
            var cacheKey = $"{_options.CacheKey}category:{id}";

            var cacheData = await _cache.GetStringAsync(cacheKey);

            if (cacheData != null)
            {
                var result = JsonSerializer.Deserialize<ExamCategoryResponse>(cacheData, serializerOptions);

                if (result != null)
                    return result;
            }

            var category = await _parentService.GetById(id);

            cacheData = JsonSerializer.Serialize(category, serializerOptions);

            await _cache.SetStringAsync(cacheKey, cacheData, _cacheEntryOptions);

            return category;
        }

        public async Task<PaginationResponse<ExamCategoryResponse>> GetPaginated(PaginatedRequest request)
        {
            return await _parentService.GetPaginated(request);
        }

        public async Task<ExamCategoryResponse> UpdateExamCategory(UpdateExamCategoryRequest request)
        {
            var result = await _parentService.UpdateExamCategory(request);

            var cacheKey = $"{_options.CacheKey}category:{request.Id}";

            var cacheData = JsonSerializer.Serialize(result, serializerOptions);

            await _cache.SetStringAsync(cacheKey, cacheData, _cacheEntryOptions);

            await _cache.RemoveAsync($"{_options.CacheKey}category:all");

            return result;
        }
    }
}
