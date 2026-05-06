using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Features.Exam.Events;
using Application.Models.Exam;
using Domain.Entity;
using MassTransit;
using System.Threading.Tasks;

namespace Application.Features.Exam.Consumers
{
    public sealed class SyncExamReadDbConsumer(IExamReadRepository examReadRepository, IExamRepository examRepository)
        : IConsumer<CreateExamEvent>, IConsumer<UpdateExamEvent>, IConsumer<DeletedExamEvent>
    {

        public async Task Consume(ConsumeContext<CreateExamEvent> context)
        {
            var message = context.Message;
            await examReadRepository.UpsertAsync(new ExamReadModel(message.ExamId, message.Title, message.Description, message.DurationInMinutes,message.ExamCategoryId,message.CreatedAt, message.UpdatedAt));
        }

        public async Task Consume(ConsumeContext<UpdateExamEvent> context)
        {
            var message = context.Message;
            await examReadRepository.UpsertAsync(new ExamReadModel(message.ExamId, message.Title, message.Description, message.DurationInMinutes, message.ExamCategoryId, message.CreatedAt, message.UpdatedAt));
        }

        public async Task Consume(ConsumeContext<DeletedExamEvent> context)
        {
            await examReadRepository.DeleteAsync(context.Message.Id, context.Message.DeletedAt);
        }
    }
}