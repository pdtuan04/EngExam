using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.Common.Interfaces;
using Application.Features.Word.Events;
using Application.Models.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Word.Commands
{
    public sealed class CreateWordCommandHandler : ICommandHandler<CreateWordCommand, WordResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public CreateWordCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }
        public async Task<WordResponse> Handle(CreateWordCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var word = new Domain.Entity.Word
            {
                Id = Guid.NewGuid(),
                Text = request.Text,
                FlashCardId = request.FlashCardId,
                CreatedAt = now,
            };
            word.UpdateMeaning(request.Meaning);
            await _unitOfWork.WordRepository.AddAsync(word);
            await _eventBus.PublishAsync(new CreateWordEvent(word.Id, word.Text, word.Meaning, word.CreatedAt, word.FlashCardId, word.CreatedAt));
            return new WordResponse(word.Id, word.Text, word.Meaning, word.CreatedAt, word.IsMemorized);
        }
    }
}
