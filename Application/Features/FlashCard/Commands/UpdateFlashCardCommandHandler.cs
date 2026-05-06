using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Common.Exceptions;
using Application.Features.FlashCard.Events;
using Application.Models.FlashCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FlashCard.Commands
{
    public sealed class UpdateFlashCardCommandHandler : ICommandHandler<UpdateFlashCardCommand, FlashCardResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public UpdateFlashCardCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }
        public async Task<FlashCardResponse> Handle(UpdateFlashCardCommand command, CancellationToken cancellationToken)
        {
            var flashCard = await _unitOfWork.FlashCardRepository.GetByIdAsync(command.Id) ?? throw new NotFoundException("FlashCard", command.Id);
            flashCard.Title = command.Title;
            flashCard.Description = command.Description;
            await _unitOfWork.FlashCardRepository.Update(flashCard);
            await _eventBus.PublishAsync(new UpdateFlashCardEvent(flashCard.Id, flashCard.Title, flashCard.Description,flashCard.CreatedAt, flashCard.UpdatedAt,flashCard.UserId));
            return new FlashCardResponse(flashCard.Id,flashCard.Title,flashCard.Description,flashCard.UserId);
        }
    }
}
