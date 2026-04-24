using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Features.ExamResult.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var examResult = await _examResultRepository.GetByIdAsync(message.Id);
            await _examResultReadRepository.UpsertAsync(examResult);
        }
    }
}
