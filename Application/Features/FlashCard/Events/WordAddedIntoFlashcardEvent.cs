using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FlashCard.Events
{
    public sealed record WordAddedIntoFlashcardEvent(Guid FlashCardId, Guid WordId);
}
