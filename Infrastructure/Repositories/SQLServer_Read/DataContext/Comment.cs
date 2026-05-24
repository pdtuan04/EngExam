using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read.DataContext
{
    public class Comment
    {
        public required Guid Id { get; set; }
        public required Guid CourseId { get; set; }
        public required string UserName { get; set; }
        public string UserAvatarUrl { get; set; }
        public string Content { get; set; }
        public required Guid RootCommentId { get; set; }
        public required string Path { get; set; }
        public int Level { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
