using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Features.Topic.Events;
using Application.Models.Topic;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Topic.Commands
{
    public sealed class CreateTopicCommandHandler : ICommandHandler<CreateTopicCommand, TopicResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public CreateTopicCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }
        public async Task<TopicResponse> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var topic = new Domain.Entity.Topic
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CreatedAt = now,
                UpdatedAt = now,
            };
            await _unitOfWork.TopicRepository.AddAsync(topic);
            await _eventBus.PublishAsync(new CreateTopicEvent(topic.Id, topic.Name, topic.Description, topic.CreatedAt, topic.UpdatedAt),cancellationToken);
            return new TopicResponse(topic.Id,topic.Name,topic.Description);
        }
    }
}
