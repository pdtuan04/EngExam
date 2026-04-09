using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Comment
    {
        public required Guid Id { get; set; }
        public required DateTime CreatedAt { get; set; }
        public Guid? ParrentCommentId { get; set; }
        public required Guid UserId { get; set; }
        public required string Content { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
