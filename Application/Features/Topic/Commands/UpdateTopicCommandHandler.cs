using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
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
            var topic = new Domain.Entity.Topic { Id = request.Id, Name = request.Name, Description = request.Description };
            await _unitOfWork.TopicRepository.Update(topic);
            await _eventBus.PublishAsync(new UpdateTopicEvent(topic.Id, topic.Name, topic.Description));
            return new TopicResponse(topic.Id,topic.Name,topic.Description);
        }
    }
}
