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
        IConsumer<DeleteFlashCardEvent>
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
            var flashCard = await _flashCardRepository.GetByIdAsync(context.Message.Id);
            if (flashCard == null)
            {
                return;
            }
            await _flashCardReadRepository.UpsertAsync(flashCard);
        }

        public async Task Consume(ConsumeContext<UpdateFlashCardEvent> context)
        {
            var flashCard = await _flashCardRepository.GetByIdAsync(context.Message.Id);
            if (flashCard == null) 
            { 
                await _flashCardReadRepository.DeleteAsync(context.Message.Id);
                return; 
            }
            await _flashCardReadRepository.UpsertAsync(flashCard);
        }

        public async Task Consume(ConsumeContext<DeleteFlashCardEvent> context)
        {
            await _flashCardReadRepository.DeleteAsync(context.Message.Id);
        }

    }
}
