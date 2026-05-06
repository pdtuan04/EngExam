using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Word
{
    public sealed record WordReadModel(
        Guid Id, 
        string Text,
        string Meaning, 
        DateTime CreatedAt,
        DateTime UpdatedAt,
        bool IsMemorized, 
        Guid FlashCardId);
}
