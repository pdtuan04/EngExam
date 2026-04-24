using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class FlashCard
    {
        public required Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Word> Words { get; set; } = new List<Word>();
        public required Guid UserId { get; set; }
    }
}
