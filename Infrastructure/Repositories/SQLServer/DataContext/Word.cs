using Domain.Abstractions.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class Word : IEntity<Guid>
    {
        public required Guid Id { get; set; }
        public required string Text { get; set; }
        public required string Meaning { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsMemorized { get; set; } = false;
        public required Guid FlashCardId { get; set; }
        public FlashCard FlashCard { get; set; } = null!;
    }
}
