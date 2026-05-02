using Application.Abstractions.Repositories.Read;
using Application.Features.Word.Events;
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
            var word = new Domain.Entity.Word
            {
                Id = message.Id,
                Text = message.Text,
                FlashCardId = message.FlashCardId,
            };
            word.UpdateMeaning(message.Meaning);
            await _wordReadRepository.UpsertAsync(word);
        }
        public async Task Consume(ConsumeContext<UpdateWordEvent> context)
        {
            var message = context.Message;
            var word = new Domain.Entity.Word
            {
                Id = message.Id,
                Text = message.Text,
                FlashCardId = message.FlashCardId,
            };
            word.UpdateMeaning(message.Meaning);
            await _wordReadRepository.UpsertAsync(word);
        }
        public async Task Consume(ConsumeContext<DeleteWordEvent> context)
        {
            await _wordReadRepository.DeleteAsync(context.Message.Id);
        }

        public async Task Consume(ConsumeContext<WordMemorizationToggledEvent> context)
        {
            var existingWord = await _wordReadRepository.GetByIdAsync(context.Message.WordId);
            if (existingWord == null)
            {
                await _wordReadRepository.DeleteAsync(existingWord.Id);
                return;
            }
            existingWord.IsMemorized = context.Message.IsMemorized;
            await _wordReadRepository.UpsertAsync(existingWord);
        }
    }
}
