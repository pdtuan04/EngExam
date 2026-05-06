using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Features.Course.Events;
using Application.Models.Course;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Course.Consumers
{
    public sealed class SyncCourseReadDbConsumer(ICourseReadRepository courseReadRepository) : IConsumer<CreateCourseEvent>,
        IConsumer<UpdateCourseEvent>,
        IConsumer<DeletedCourseEvent>
    {
        public async Task Consume(ConsumeContext<CreateCourseEvent> context)
        {
            var message = context.Message;
            await courseReadRepository.UpsertAsync(new CourseReadModel(
                message.CourseId, 
                message.Name, 
                message.Description, 
                message.Content, 
                message.ImageUrl, 
                message.TopicId,
                message.CreatedAt, 
                message.UpdatedAt));
        }

        public async Task Consume(ConsumeContext<DeletedCourseEvent> context)
        {
            var message = context.Message;
            await courseReadRepository.DeleteAsync(message.Id, message.DeletedAt);
        }

        public async Task Consume(ConsumeContext<UpdateCourseEvent> context)
        {
            var message = context.Message;
            await courseReadRepository.UpsertAsync(new CourseReadModel(
                message.CourseId,
                message.Name,
                message.Description,
                message.Content,
                message.ImageUrl,
                message.TopicId,
                message.CreatedAt,
                message.UpdatedAt));
        }
    }
}
