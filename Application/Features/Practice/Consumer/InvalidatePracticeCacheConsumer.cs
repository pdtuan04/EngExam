using Application.Abstractions.Caching;
using Application.Common.Caching;
using Application.Features.Practice.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Practice.Consumer
{
    public sealed class InvalidatePracticeCacheConsumer : IConsumer<UpdatePracticeEvent>
    {
        private readonly ICacheService _cacheService;
        public InvalidatePracticeCacheConsumer(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task Consume(ConsumeContext<UpdatePracticeEvent> context)
        {
            var message = context.Message;
            await _cacheService.RemoveCacheAsync(CacheKeys.PracticeToTake(message.Practice.Id),context.CancellationToken);
            await _cacheService.RemoveCacheAsync(CacheKeys.PracticeDetails(message.Practice.Id), context.CancellationToken);
        }
    }
}
