using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.Common.Exceptions;
using Application.Features.Topic.Events;
using Application.Models.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Topic.Commands
{
    public sealed class UpdateTopicCommandHandler : ICommandHandler<UpdateTopicCommand, TopicResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public UpdateTopicCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }

        public async Task<TopicResponse> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
        {
            var topic = await _unitOfWork.TopicRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Topic", request.Id);
            var now = DateTime.UtcNow;
            topic.Name = request.Name;
            topic.Description = request.Description;
            topic.UpdatedAt = now;
            await _unitOfWork.TopicRepository.Update(topic);
            await _eventBus.PublishAsync(new UpdateTopicEvent(topic.Id, topic.Name, topic.Description, topic.CreatedAt, topic.UpdatedAt), cancellationToken);
            return new TopicResponse(topic.Id,topic.Name,topic.Description);
        }
    }
}
