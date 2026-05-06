using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FlashCard.Events
{
    public sealed record CreateFlashCardEvent(Guid Id, string Title, string Description,DateTime CreatedAt, DateTime UpdatedAt, Guid UserId);
}
