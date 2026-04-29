using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Features.FlashCard.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FlashCard.Consumers
{
    public sealed class SyncFlashCardReadDbConsumer :
        IConsumer<CreateFlashCardEvent>,
        IConsumer<UpdateFlashCardEvent>,
        IConsumer<DeleteFlashCardEvent>,
        IConsumer<WordAddedIntoFlashcardEvent>,
        IConsumer<WordRemovedFromFlashcardEvent>
    {
        private readonly IFlashCardRepository _flashCardRepository;
        private readonly IFlashCardReadRepository _flashCardReadRepository;
        public SyncFlashCardReadDbConsumer(IFlashCardRepository flashCardRepository, IFlashCardReadRepository flashCardReadRepository)
        {
            _flashCardRepository = flashCardRepository;
            _flashCardReadRepository = flashCardReadRepository;
        }
        public async Task Consume(ConsumeContext<CreateFlashCardEvent> context)
        {
            await _flashCardReadRepository.UpsertAsync(new Domain.Entity.FlashCard
            {
                Id = context.Message.Id,
                Title = context.Message.Title,
                Description = context.Message.Description,
                UserId = context.Message.UserId,
                CreatedAt = context.Message.CreatedAt,
            });
        }

        public async Task Consume(ConsumeContext<UpdateFlashCardEvent> context)
        {
            await _flashCardReadRepository.UpsertAsync(new Domain.Entity.FlashCard
            {
                Id = context.Message.Id,
                Title = context.Message.Title,
                Description = context.Message.Description,
                UserId = context.Message.UserId
            });
        }

        public async Task Consume(ConsumeContext<DeleteFlashCardEvent> context)
        {
            await _flashCardReadRepository.DeleteAsync(context.Message.Id);
        }

        public async Task Consume(ConsumeContext<WordAddedIntoFlashcardEvent> context)
        {
            var flashCard = await _flashCardRepository.GetFlashCardDetailAsync(context.Message.FlashCardId);
            await _flashCardReadRepository.UpsertAsync(flashCard);
        }

        public async Task Consume(ConsumeContext<WordRemovedFromFlashcardEvent> context)
        {
            var flashCard = await _flashCardRepository.GetFlashCardDetailAsync(context.Message.FlashCardId);
            await _flashCardReadRepository.UpsertAsync(flashCard);
        }
    }
}
