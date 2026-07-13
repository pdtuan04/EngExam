using Application.Abstractions.Caching;
using Application.Common.Caching;
using Application.Features.User.Commands;
using Application.Features.User.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Consumers
{
    public sealed class InvalidateUserCacheConsumer : IConsumer<UserAvatarUpdatedEvent>
    {
        private readonly ICacheService _cacheService;
        public InvalidateUserCacheConsumer(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task Consume(ConsumeContext<UserAvatarUpdatedEvent> context)
        {
            await _cacheService.RemoveCacheAsync(CacheKeys.UserDetail(context.Message.UserId));
        }
    }
}
