using Application.Abstractions.Caching;
using Application.Common.Caching;
using Application.Features.Topic.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Topic.Consumers
{
    public sealed class InvalidateTopicCacheConsumer : IConsumer<UpdateTopicEvent>, IConsumer<DeleteTopicEvent>
    {
        private readonly ICacheService _cacheService;
        public InvalidateTopicCacheConsumer(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task Consume(ConsumeContext<UpdateTopicEvent> context)
        {
            var topicId = context.Message.Id;
            await _cacheService.RemoveCacheAsync(CacheKeys.TopicDetail(topicId));
        }

        public async Task Consume(ConsumeContext<DeleteTopicEvent> context)
        {
            var topicId = context.Message.Id;
            await _cacheService.RemoveCacheAsync(CacheKeys.TopicDetail(topicId));
        }
    }
}
