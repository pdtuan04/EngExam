using Application.Abstractions.Caching;
using Application.Common.Caching;
using Application.Features.Exam.Events;
using Domain.Entity;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Application.Features.Exam.Consumers
{
    public class InvalidateExamCacheConsumer : 
        IConsumer<UpdateExamEvent>, 
        IConsumer<DeletedExamEvent>
    {
        private readonly ILogger<InvalidateExamCacheConsumer> _logger;
        private readonly ICacheService _cacheService;

        public InvalidateExamCacheConsumer(ILogger<InvalidateExamCacheConsumer> logger, ICacheService cacheService)
        {
            _logger = logger;
            _cacheService = cacheService;
        }
        public async Task Consume(ConsumeContext<UpdateExamEvent> context)
        {
            var message = context.Message;
            await _cacheService.RemoveCacheAsync(CacheKeys.ExamByCategory(message.ExamCategoryId), context.CancellationToken);
            await _cacheService.RemoveCacheAsync(CacheKeys.ExamDetail(message.ExamId), context.CancellationToken);
        }

        public async Task Consume(ConsumeContext<DeletedExamEvent> context)
        {
            var message = context.Message;
            await _cacheService.RemoveCacheAsync(CacheKeys.ExamDetail(message.Id), context.CancellationToken);
            _logger.LogInformation($"Exam cache cleared after deleting Exam: {message.Id}.");
        }
    }
}