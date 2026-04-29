using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Common.Exceptions;
using Application.Features.FlashCard.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FlashCard.Commands
{
    public sealed class DeleteFlashCardCommandHandler : ICommandHandler<DeleteFlashCardCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public DeleteFlashCardCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }
        public async Task<bool> Handle(DeleteFlashCardCommand request, CancellationToken cancellationToken)
        {
            var flashCard = await _unitOfWork.FlashCardRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("FlashCard",request.Id);
            await _unitOfWork.FlashCardRepository.Delete(request.Id);
            await _eventBus.PublishAsync(new DeleteFlashCardEvent(flashCard.Id, flashCard.Id));
            return true;
        }
    }
}
