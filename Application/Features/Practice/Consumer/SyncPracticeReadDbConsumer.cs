using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Features.Practice.Events;
using Application.Models.Practice;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Practice.Consumer
{
    public sealed class SyncPracticeReadDbConsumer : IConsumer<CreatePracticeEvent>, IConsumer<UpdatePracticeEvent>, IConsumer<DeletePracticeEvent>
    {
        private readonly IPracticeRepository _practiceRepository;
        private readonly IPracticeReadRepository _practiceReadRepository;
        public SyncPracticeReadDbConsumer(IPracticeRepository practiceRepository, IPracticeReadRepository practiceReadRepository)
        {
            _practiceRepository = practiceRepository;
            _practiceReadRepository = practiceReadRepository;
        }
        public async Task Consume(ConsumeContext<CreatePracticeEvent> context)
        {
            var message = context.Message;
            await _practiceReadRepository.UpsertAsync(new PracticeReadModel(message.PracticeId, message.Title, message.Description, message.TopicId, message.CreatedAt, message.UpdatedAt));
        }

        public async Task Consume(ConsumeContext<UpdatePracticeEvent> context)
        {
            var message = context.Message;
            await _practiceReadRepository.UpsertAsync(new PracticeReadModel(message.PracticeId, message.Title, message.Description, message.TopicId, message.CreatedAt, message.UpdatedAt));
        }

        public async Task Consume(ConsumeContext<DeletePracticeEvent> context)
        {
            var message = context.Message;
            await _practiceReadRepository.DeleteAsync(message.PracticeId, message.DeletedAt);
        }
    }
}
