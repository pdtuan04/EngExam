using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FlashCard.Events
{
    public sealed record DeleteFlashCardEvent(Guid Id, Guid UserId, DateTime DeletedAt);
}
