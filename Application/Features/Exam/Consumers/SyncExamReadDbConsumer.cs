using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Features.Exam.Events;
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
            var examExists = await examRepository.GetExamDetail(message.ExamId);
            await examReadRepository.UpsertAsync(examExists);
        }

        public async Task Consume(ConsumeContext<UpdateExamEvent> context)
        {
            var message = context.Message;
            var examExists = await examRepository.GetExamDetail(message.ExamId);
            await examReadRepository.UpsertAsync(examExists);
        }

        public async Task Consume(ConsumeContext<DeletedExamEvent> context)
        {
            await examReadRepository.DeleteAsync(context.Message.Id);
        }
    }
}