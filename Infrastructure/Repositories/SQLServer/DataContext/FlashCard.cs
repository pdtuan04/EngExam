using Domain.Abstractions.Entity;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class FlashCard : IEntity<Guid>, ISoftDeletable
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Word> Words { get; set; } = null!;
        public required Guid UserId { get; set; }
        public bool IsDeleted { get ; set ; }
    }
}
