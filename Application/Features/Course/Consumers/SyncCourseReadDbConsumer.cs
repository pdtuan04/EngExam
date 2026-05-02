using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Features.Course.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Course.Consumers
{
    public sealed class SyncCourseReadDbConsumer(ICourseReadRepository courseReadRepository, ICourseRepository courseRepository) : IConsumer<CreateCourseEvent>,
        IConsumer<UpdateCourseEvent>,
        IConsumer<DeletedCourseEvent>
    {
        public async Task Consume(ConsumeContext<CreateCourseEvent> context)
        {
            var message = context.Message;
            var course = await courseRepository.GetByIdAsync(message.CourseId);
            if (course == null)
            {
                return;
            }
            await courseReadRepository.UpsertAsync(course);
        }

        public async Task Consume(ConsumeContext<DeletedCourseEvent> context)
        {
            var message = context.Message;
            await courseReadRepository.DeleteAsync(message.Id);
        }

        public async Task Consume(ConsumeContext<UpdateCourseEvent> context)
        {
            var message = context.Message;
            var course = await courseRepository.GetByIdAsync(message.CourseId);
            if (course == null)
            {
                await courseReadRepository.DeleteAsync(message.CourseId);
                return;
            }
            await courseReadRepository.UpsertAsync(course);
        }
    }
}
