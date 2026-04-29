using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Features.FlashCard.Events;
using Application.Models.FlashCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FlashCard.Commands
{
    public sealed class CreateFlashCardCommandHandler : ICommandHandler<CreateFlashCardCommand, FlashCardResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public CreateFlashCardCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }
        public async Task<FlashCardResponse> Handle(CreateFlashCardCommand request, CancellationToken cancellationToken)
        {
            var flashCard = new Domain.Entity.FlashCard
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
            };
            await _unitOfWork.FlashCardRepository.AddAsync(flashCard);
            await _eventBus.PublishAsync(new CreateFlashCardEvent(flashCard.Id, flashCard.Title, flashCard.Description, flashCard.CreatedAt, flashCard.UserId));
            return new FlashCardResponse(flashCard.Id,flashCard.Title,flashCard.Description,flashCard.UserId);
        }
    }
}
