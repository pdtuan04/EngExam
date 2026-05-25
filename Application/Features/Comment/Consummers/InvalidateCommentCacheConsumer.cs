using Application.Abstractions.Caching;
using Application.Common.Caching;
using Application.Features.Comment.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Consummers
{
    public sealed class InvalidateCommentCacheConsumer : IConsumer<CreateCommentEvent>, IConsumer<DeleteCommentEvent>
    {
        private readonly ICacheService _cacheService;
        public InvalidateCommentCacheConsumer(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task Consume(ConsumeContext<CreateCommentEvent> context)
        {
            await _cacheService.RemoveCacheAsync(CacheKeys.CourseComments(context.Message.CourseId));
        }

        public async Task Consume(ConsumeContext<DeleteCommentEvent> context)
        {
            await _cacheService.RemoveCacheAsync(CacheKeys.CourseComments(context.Message.CourseId));
        }
    }
}
