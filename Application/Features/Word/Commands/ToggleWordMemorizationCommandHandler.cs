using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Common.Exceptions;
using Application.Features.Word.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Word.Commands
{
    public sealed class ToggleWordMemorizationCommandHandler : ICommandHandler<ToggleWordMemorizationCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public ToggleWordMemorizationCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }
        public async Task<bool> Handle(ToggleWordMemorizationCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            await _eventBus.PublishAsync(new WordMemorizationToggledEvent(request.Id, request.IsMemorized, now, request.FlashCardId));
            return request.IsMemorized;
        }
    }
}
