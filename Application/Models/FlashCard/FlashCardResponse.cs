using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.FlashCard
{
    public sealed record FlashCardResponse(Guid Id, string Title, string? Description, Guid UserId);
}
