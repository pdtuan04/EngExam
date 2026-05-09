using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Features.Practice.Events;
using Application.Models.Practice;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Practice.Consumer
{
    public sealed class SyncPracticeReadDbConsumer : IConsumer<CreatePracticeEvent>, IConsumer<UpdatePracticeEvent>, IConsumer<DeletePracticeEvent>
    {
        private readonly IPracticeReadRepository _practiceReadRepository;
        private readonly IQuestionReadRepository _questionReadRepository;
        private readonly IAnswerReadRepository _answerReadRepository;
        public SyncPracticeReadDbConsumer(IPracticeReadRepository practiceReadRepository, IQuestionReadRepository questionReadRepository, IAnswerReadRepository answerReadRepository)
        {
            _practiceReadRepository = practiceReadRepository;
            _questionReadRepository = questionReadRepository;
            _answerReadRepository = answerReadRepository;
        }
        public async Task Consume(ConsumeContext<CreatePracticeEvent> context)
        {
            var message = context.Message;
            await _practiceReadRepository.UpsertAsync(message.Practice);
            await _questionReadRepository.UpsertBulkAsync(message.Questions);
            await _answerReadRepository.UpsertBulkAsync(message.Answers);
            await _practiceReadRepository.UpsertPracticeDetailsAsync(message.Details);
        }

        public async Task Consume(ConsumeContext<UpdatePracticeEvent> context)
        {
            var message = context.Message;
            await _practiceReadRepository.UpsertAsync(message.Practice);
            await _practiceReadRepository.UpsertPracticeDetailsAsync(message.Details);

        }

        public async Task Consume(ConsumeContext<DeletePracticeEvent> context)
        {
            var message = context.Message;
            await _practiceReadRepository.DeleteAsync(message.PracticeId, message.DeletedAt);
        }
    }
}
