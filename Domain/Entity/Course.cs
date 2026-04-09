using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Course : BaseEntity
    {
        public required string Name { get; set; }
        public string Description { get; set; } = null!;
        public required string Content { get; set; }
        public string? ImageUrl { get; set; }
        public required Guid TopicId { get; set; }
    }
}
