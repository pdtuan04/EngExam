using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Features.FlashCard.Events;
using Application.Models.FlashCard;
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
            var message = context.Message;

            await _flashCardReadRepository.UpsertAsync(new FlashCardReadModel(message.Id, message.Title, message.Description, message.CreatedAt, message.UpdatedAt, message.UserId));
        }

        public async Task Consume(ConsumeContext<UpdateFlashCardEvent> context)
        {
            var message = context.Message;
            await _flashCardReadRepository.UpsertAsync(new FlashCardReadModel(message.Id, message.Title, message.Description, message.CreatedAt, message.UpdatedAt, message.UserId));
        }

        public async Task Consume(ConsumeContext<DeleteFlashCardEvent> context)
        {
            await _flashCardReadRepository.DeleteAsync(context.Message.Id, context.Message.DeletedAt);
        }

    }
}
