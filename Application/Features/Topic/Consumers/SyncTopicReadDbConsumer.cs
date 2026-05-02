using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Features.Topic.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Topic.Consumers
{
    public sealed class SyncTopicReadDbConsumer : IConsumer<CreateTopicEvent>, IConsumer<UpdateTopicEvent>, IConsumer<DeleteTopicEvent>
    {
        private readonly ITopicReadRepository _topicReadRepository;
        private readonly ITopicRepository _topicRepository;
        public SyncTopicReadDbConsumer(ITopicReadRepository topicReadRepository, ITopicRepository topicRepository)
        {
            _topicReadRepository = topicReadRepository;
            _topicRepository = topicRepository;
        }
        public async Task Consume(ConsumeContext<CreateTopicEvent> context)
        {
            var message = context.Message;
            var topic = await _topicRepository.GetByIdAsync(message.Id);
            if (topic == null)
            {
                return;
            }
            await _topicReadRepository.UpsertAsync(topic);
        }

        public async Task Consume(ConsumeContext<UpdateTopicEvent> context)
        {
            var message = context.Message;
            var topic = await _topicRepository.GetByIdAsync(message.Id);
            if(topic == null)
            {
                await _topicReadRepository.DeleteAsync(message.Id);
                return;
            }
            await _topicReadRepository.UpsertAsync(topic);
        }

        public async Task Consume(ConsumeContext<DeleteTopicEvent> context)
        {
            var message = context.Message;
            await _topicReadRepository.DeleteAsync(message.Id);
        }
    }
}
