using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class QuestionGroup : BaseEntity<Guid>, ISoftDeletable
    {
        public string? Title { get; set; }
        public required string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? AudioUrl { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public bool IsDeleted { get; set; }
    }
}
