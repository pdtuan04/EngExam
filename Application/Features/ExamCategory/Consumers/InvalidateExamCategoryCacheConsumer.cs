using Application.Abstractions.Caching;
using Application.Common.Caching;
using Application.Features.ExamCategory.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamCategory.Consumers
{
    public class InvalidateExamCategoryCacheConsumer : IConsumer<CreateExamCategoryEvent>, IConsumer<UpdateExamCategoryEvent>, IConsumer<DeletedExamCategoryEvent>
    {
        private readonly ILogger<InvalidateExamCategoryCacheConsumer> _logger;
        private readonly ICacheService _cacheService;
        public InvalidateExamCategoryCacheConsumer(ILogger<InvalidateExamCategoryCacheConsumer> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }
        public async Task Consume(ConsumeContext<CreateExamCategoryEvent> context)
        {
            await _cacheService.RemoveCacheAsync(CacheKeys.AllExamCategories, context.CancellationToken);
            _logger.LogInformation("Exam category cache removed successfully.///////////////////////////////////");
        }

        public async Task Consume(ConsumeContext<UpdateExamCategoryEvent> context)
        {
            await _cacheService.RemoveCacheAsync(CacheKeys.AllExamCategories, context.CancellationToken);
            _logger.LogInformation("Exam category cache removed successfully.///////////////////////////////////");
        }

        public async Task Consume(ConsumeContext<DeletedExamCategoryEvent> context)
        {
            await _cacheService.RemoveCacheAsync(CacheKeys.AllExamCategories, context.CancellationToken);
            _logger.LogInformation("Exam category cache removed successfully.///////////////////////////////////");
        }
    }
}
