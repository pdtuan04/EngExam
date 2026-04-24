using Application.Abstractions.Caching;
using Application.Common.Caching;
using Application.Features.Course.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Course.Consumers
{
    public sealed class InvalidateCourseCacheConsumer : IConsumer<UpdateCourseEvent>, IConsumer<DeletedCourseEvent>
    {
        private readonly ICacheService _cacheService;

        public InvalidateCourseCacheConsumer(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task Consume(ConsumeContext<UpdateCourseEvent> context)
        {
            var message = context.Message;
            await _cacheService.RemoveCacheAsync(CacheKeys.CourseDetail(message.CourseId));
        }

        public async Task Consume(ConsumeContext<DeletedCourseEvent> context)
        {
            var message = context.Message;
            await _cacheService.RemoveCacheAsync(CacheKeys.CourseDetail(message.Id));
        }
    }
}