using Application.Abstractions.Repositories.Read;
using Application.Features.ExamCategory.Events;
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
            var examCategory = new Domain.Entity.ExamCategory
            {
                Id = message.CategoryId,
                Name = message.Name,
                Description = message.Description,
                ImageUrl = message.ImageUrl,
                IsActive = message.IsActive,
            };
            await examCategoryReadRepository.UpsertAsync(examCategory);
        }

        public async Task Consume(ConsumeContext<DeletedExamCategoryEvent> context)
        {
            var message = context.Message;
            await examCategoryReadRepository.DeleteAsync(message.Id);
        }

        public async Task Consume(ConsumeContext<UpdateExamCategoryEvent> context)
        {
            var message = context.Message;
            var examCategory = new Domain.Entity.ExamCategory
            {
                Id = message.CategoryId,
                Name = message.Name,
                Description = message.Description,
                ImageUrl = message.ImageUrl,
                UpdatedAt = message.UpdatedAt,
                IsActive = message.IsActive,
            };
            await examCategoryReadRepository.UpsertAsync(examCategory);
        }
    }
}
