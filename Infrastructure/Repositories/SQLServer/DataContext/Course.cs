using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class Course : BaseEntity
    {
        public required string Name { get; set; }
        public string Description { get; set; } = null!;
        public required string Content { get; set; }
        public string? ImageUrl { get; set; }
        public required Guid TopicId { get; set; }
        public Topic? Topic { get; set; }
    }
}
