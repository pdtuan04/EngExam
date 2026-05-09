using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Features.Exam.Events;
using Application.Models.Answer;
using Application.Models.Exam;
using Application.Models.Question;
using Domain.Entity;
using MassTransit;
using System.Threading.Tasks;

namespace Application.Features.Exam.Consumers
{
    public sealed class SyncExamReadDbConsumer(
        IExamReadRepository examReadRepository, 
        IQuestionReadRepository questionReadRepository,
        IAnswerReadRepository answerReadRepository)
        : IConsumer<CreateExamEvent>, IConsumer<UpdateExamEvent>, IConsumer<DeletedExamEvent>
    {

        public async Task Consume(ConsumeContext<CreateExamEvent> context)
        {
            var message = context.Message;
            await examReadRepository.UpsertAsync(new ExamReadModel(message.Exam.Id, message.Exam.Title, message.Exam.Description, message.Exam.DurationInMinutes, message.Exam.ExamCategoryId, message.Exam.CreatedAt, message.Exam.UpdatedAt));
            await questionReadRepository.UpsertBulkAsync(message.Questions);
            await answerReadRepository.UpsertBulkAsync(message.Answers);
            await examReadRepository.UpsertExamDetailsAsync(message.ExamDetails, message.Exam.Id);
        }

        public async Task Consume(ConsumeContext<UpdateExamEvent> context)
        {
            var message = context.Message;
            await examReadRepository.UpsertAsync(new ExamReadModel(message.Exam.Id, message.Exam.Title, message.Exam.Description, message.Exam.DurationInMinutes, message.Exam.ExamCategoryId, message.Exam.CreatedAt, message.Exam.UpdatedAt));
            await questionReadRepository.UpsertBulkAsync(message.Questions);
            await answerReadRepository.UpsertBulkAsync(message.Answers);
            await examReadRepository.UpsertExamDetailsAsync(message.ExamDetails, message.Exam.Id);
        }

        public async Task Consume(ConsumeContext<DeletedExamEvent> context)
        {
            await examReadRepository.DeleteAsync(context.Message.Id, context.Message.DeletedAt);
        }
    }
}