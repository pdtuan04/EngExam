using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read.DataContext
{
    public class Word : ISoftDeletable
    {
        public required Guid Id { get; set; }
        public required string Text { get; set; }
        public required string Meaning { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }
        public bool IsMemorized { get; set; } = false;
        public required Guid FlashCardId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
