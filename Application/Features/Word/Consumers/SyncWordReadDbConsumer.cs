using Application.Abstractions.Repositories.Read;
using Application.Features.Word.Events;
using Application.Models.Word;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Word.Consumers
{
    public sealed class SyncWordReadDbConsumer : 
        IConsumer<CreateWordEvent>, 
        IConsumer<UpdateWordEvent>, 
        IConsumer<DeleteWordEvent>,
        IConsumer<WordMemorizationToggledEvent>
    {
        private readonly IWordReadRepository _wordReadRepository;
        public SyncWordReadDbConsumer(IWordReadRepository wordReadRepository)
        {
            _wordReadRepository = wordReadRepository;
        }
        public async Task Consume(ConsumeContext<CreateWordEvent> context)
        {
            var message = context.Message;
            var word = new WordReadModel(message.Id,message.Text,message.Meaning,message.CreatedAt, message.CreatedAt, false,message.FlashCardId);
            await _wordReadRepository.UpsertAsync(word);
        }
        public async Task Consume(ConsumeContext<UpdateWordEvent> context)
        {
            var message = context.Message;
            var word = new WordReadModel(message.Id, message.Text, message.Meaning, message.CreatedAt, message.UpdateAt,false, message.FlashCardId);
            await _wordReadRepository.UpsertAsync(word);
        }
        public async Task Consume(ConsumeContext<DeleteWordEvent> context)
        {
            await _wordReadRepository.DeleteAsync(context.Message.Id, context.Message.ActionAt);
        }

        public async Task Consume(ConsumeContext<WordMemorizationToggledEvent> context)
        {
            await _wordReadRepository.ToggleWordMemorization(context.Message.WordId, context.Message.IsMemorized, context.Message.ActionAt);
        }
    }
}
