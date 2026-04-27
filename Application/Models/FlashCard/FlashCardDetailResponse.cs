using Application.Models.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.FlashCard
{
    public sealed record FlashCardDetailResponse(
        Guid Id, 
        string Title,
        Guid UserId,
        string? Description,
        IReadOnlyCollection<WordResponse> Words);
}
