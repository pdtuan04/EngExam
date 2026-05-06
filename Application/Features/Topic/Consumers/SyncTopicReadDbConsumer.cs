using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Features.Topic.Events;
using Application.Models.Topic;
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
            await _topicReadRepository.UpsertAsync(new TopicReadModel(
                message.Id,
                message.Name,
                message.Description,
                message.CreatedAt,
                message.UpdatedAt
            ));
        }

        public async Task Consume(ConsumeContext<UpdateTopicEvent> context)
        {
            var message = context.Message;
            await _topicReadRepository.UpsertAsync(new TopicReadModel(
                message.Id,
                message.Name,
                message.Description,
                message.CreatedAt,
                message.UpdatedAt
            ));
        }

        public async Task Consume(ConsumeContext<DeleteTopicEvent> context)
        {
            var message = context.Message;
            await _topicReadRepository.DeleteAsync(message.Id, message.DeletedAt);
        }
    }
}
