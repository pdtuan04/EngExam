using Application.Abstractions.Caching;
using Application.Common.Caching;
using Application.Features.Word.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Word.Consumers
{
    public sealed class InvalidateWordCacheConsumer : IConsumer<CreateWordEvent>, IConsumer<UpdateWordEvent>, IConsumer<DeleteWordEvent>
    {
        private readonly ICacheService _cacheService;
        public InvalidateWordCacheConsumer(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task Consume(ConsumeContext<DeleteWordEvent> context)
        {
            await _cacheService.RemoveCacheAsync(CacheKeys.WordMeaning(context.Message.Text));
        }
        public async Task Consume(ConsumeContext<UpdateWordEvent> context)
        {
            await _cacheService.RemoveCacheAsync(CacheKeys.WordMeaning(context.Message.Text));
        }

        public async Task Consume(ConsumeContext<CreateWordEvent> context)
        {
            await _cacheService.RemoveCacheAsync(CacheKeys.WordMeaning(context.Message.Text));
        }
    }
}
