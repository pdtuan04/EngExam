using Application.Abstractions.Caching;
using Application.Common.Caching;
using Application.Features.FlashCard.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FlashCard.Consumers
{
    public sealed class InvalidateFlashCardCacheConsumer : 
        IConsumer<CreateFlashCardEvent>,
        IConsumer<UpdateFlashCardEvent>,
        IConsumer<DeleteFlashCardEvent>
    {
        private readonly ICacheService _cacheService;
        public InvalidateFlashCardCacheConsumer(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Consume(ConsumeContext<CreateFlashCardEvent> context)
        {
            await _cacheService.RemoveCacheAsync(CacheKeys.FlashCardsByUser(context.Message.UserId));
        }
        public async Task Consume(ConsumeContext<UpdateFlashCardEvent> context)
        {
            await _cacheService.RemoveCacheAsync(CacheKeys.FlashCardDetail(context.Message.Id));
        }

        public async Task Consume(ConsumeContext<DeleteFlashCardEvent> context)
        {
            await _cacheService.RemoveCacheAsync(CacheKeys.FlashCardDetail(context.Message.Id));
            await _cacheService.RemoveCacheAsync(CacheKeys.FlashCardsByUser(context.Message.UserId));
        }
    }
}