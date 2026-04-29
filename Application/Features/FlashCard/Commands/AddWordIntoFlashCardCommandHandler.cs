using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Features.FlashCard.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FlashCard.Commands
{
    public sealed class AddWordIntoFlashCardCommandHandler : ICommandHandler<AddWordIntoFlashCardCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public AddWordIntoFlashCardCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }
        public async Task<bool> Handle(AddWordIntoFlashCardCommand request, CancellationToken cancellationToken)
        {
            var flashCard = await _unitOfWork.FlashCardRepository.GetByIdAsync(request.FlashCardId);
            var word = await _unitOfWork.WordRepository.GetByIdAsync(request.WordId);
            flashCard.AddWord(word);
            await _unitOfWork.FlashCardRepository.Update(flashCard);
            await _eventBus.PublishAsync(new WordAddedIntoFlashcardEvent(flashCard.Id, word.Id));
            return true;
        }
    }
}
