using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Features.ExamResult.Events;
using Application.Models.ExamResult;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Features.ExamResult.Consumers
{
    public sealed class SyncExamResultReadDbConsumer : IConsumer<CreateExamResultEvent>
    {
        private readonly IExamResultReadRepository _examResultReadRepository;
        private readonly IExamResultRepository _examResultRepository;
        public SyncExamResultReadDbConsumer(IExamResultReadRepository examResultReadRepository, IExamResultRepository examResultRepository)
        {
            _examResultReadRepository = examResultReadRepository;
            _examResultRepository = examResultRepository;   
        }
        public async Task Consume(ConsumeContext<CreateExamResultEvent> context)
        {
            var message = context.Message;
            var examResult = await _examResultRepository.GetDetailByIdAsync(message.Id);
            if (examResult == null)
            {
                return;
            }
            var examResultReadModel = new ExamResultReadModel(
                examResult.Id,
                examResult.Exam.Title,
                examResult.Exam.Description,
                examResult.Exam.DurationInMinutes,
                examResult.CompleteAt,
                examResult.Score,
                examResult.ExamId,
                examResult.UserId
            );
            await _examResultReadRepository.UpsertAsync(examResultReadModel);
        }
    }
}
