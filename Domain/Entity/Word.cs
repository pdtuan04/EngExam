using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public sealed class Word
    {
        public required Guid Id { get; init; }
        public required string Text { get; init; }
        public required IEnumerable<string> Meanings { get; init; }
    }
}
