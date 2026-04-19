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
    public class UpdateExamCategoryCacheConsumer : IConsumer<CreateExamCategoryEvent>
    {
        private readonly ILogger<UpdateExamCategoryCacheConsumer> _logger;
        private readonly ICacheService _cacheService;
        public UpdateExamCategoryCacheConsumer(ILogger<UpdateExamCategoryCacheConsumer> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }
        public async Task Consume(ConsumeContext<CreateExamCategoryEvent> context)
        {
            await _cacheService.RemoveCacheAsync(CacheKeys.ExamCategories_All, context.CancellationToken);
            _logger.LogInformation("Exam category cache removed successfully.///////////////////////////////////");
        }
    }
}
