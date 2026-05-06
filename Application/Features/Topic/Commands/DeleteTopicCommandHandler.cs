using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Features.Topic.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Topic.Commands
{
    public sealed class DeleteTopicCommandHandler : ICommandHandler<DeleteTopicCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public DeleteTopicCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }
        public async Task<bool> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var result = await _unitOfWork.TopicRepository.Delete(request.Id);
            await _eventBus.PublishAsync(new DeleteTopicEvent(request.Id, now), cancellationToken);
            return result;
        }
    }
}
