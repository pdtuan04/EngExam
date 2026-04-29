using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FlashCard.Commands
{
    public record RemoveWordFromFlashcardCommand(Guid FlashCardId, Guid WordId) : ICommand<bool>;
}
