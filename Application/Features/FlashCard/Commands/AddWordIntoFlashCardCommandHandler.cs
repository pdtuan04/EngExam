using Application.Abstractions;
using Application.Abstractions.Messaging;
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
        public AddWordIntoFlashCardCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(AddWordIntoFlashCardCommand request, CancellationToken cancellationToken)
        {
            var flashCard = await _unitOfWork.FlashCardRepository.GetByIdAsync(request.FlashCardId);
            var word = await _unitOfWork.WordRepository.GetByIdAsync(request.WordId);
            flashCard.AddWord(word);
            return true;
        }
    }
}
