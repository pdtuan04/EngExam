using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Practice : BaseEntity, ISoftDeletable
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public ICollection<PracticeDetail> PracticeDetails { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public required Guid TopicId { get; set; }
    }
}
