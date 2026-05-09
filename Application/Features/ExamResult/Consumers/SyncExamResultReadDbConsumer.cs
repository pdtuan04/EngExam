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
        public SyncExamResultReadDbConsumer(IExamResultReadRepository examResultReadRepository)
        {
            _examResultReadRepository = examResultReadRepository;
        }
        public async Task Consume(ConsumeContext<CreateExamResultEvent> context)
        {
            var message = context.Message;
            await _examResultReadRepository.UpsertAsync(message.ExamResult);
        }
    }
}
