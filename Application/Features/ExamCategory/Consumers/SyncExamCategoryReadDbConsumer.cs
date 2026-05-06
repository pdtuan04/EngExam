using Application.Abstractions.Repositories.Read;
using Application.Features.ExamCategory.Events;
using Application.Models.ExamCategory;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamCategory.Consumers
{
    public sealed class SyncExamCategoryReadDbConsumer(IExamCategoryReadRepository examCategoryReadRepository)
        : IConsumer<CreateExamCategoryEvent>, IConsumer<UpdateExamCategoryEvent>, IConsumer<DeletedExamCategoryEvent>
    {
        public async Task Consume(ConsumeContext<CreateExamCategoryEvent> context)
        {
            var message = context.Message;
            await examCategoryReadRepository.UpsertAsync(new ExamCategoryReadModel(message.CategoryId, message.Name, message.Description, message.ImageUrl, message.CreatedAt, message.UpdatedAt));
        }

        public async Task Consume(ConsumeContext<DeletedExamCategoryEvent> context)
        {
            var message = context.Message;
            await examCategoryReadRepository.DeleteAsync(message.Id, message.DeletedAt);
        }

        public async Task Consume(ConsumeContext<UpdateExamCategoryEvent> context)
        {
            var message = context.Message;
            await examCategoryReadRepository.UpsertAsync(new ExamCategoryReadModel(message.CategoryId, message.Name, message.Description, message.ImageUrl, message.CreatedAt, message.UpdatedAt));
        }
    }
}
