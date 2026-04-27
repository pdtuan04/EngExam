using Application.Abstractions;
using Application.Abstractions.Messaging;
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
        public CreateFlashCardCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<FlashCardResponse> Handle(CreateFlashCardCommand request, CancellationToken cancellationToken)
        {
            var flashCard = new Domain.Entity.FlashCard
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId
            };
            await _unitOfWork.FlashCardRepository.AddAsync(flashCard);
            return new FlashCardResponse(flashCard.Id,flashCard.Title,flashCard.Description,flashCard.UserId);
        }
    }
}
