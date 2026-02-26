using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class Practice : BaseEntity
    {
        public string? Description { get; set; }
        public required Guid TopicId { get; set; }
        public Topic Topic { get; set; } = null!;
        public ICollection<PracticeDetail> PracticeDetails { get; set; } = null!;
    }
}
