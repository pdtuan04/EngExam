using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class Comment : BaseEntity<Guid>, ISoftDeletable
    {
        public required Guid CourseId { get; set; }
        public required Guid UserId { get; set; }
        public required string Content { get; set; }
        public Guid? ParentId { get; set; }
        public required Guid RootCommentId { get; set; }
        public required string Path { get; set; }
        public required int Level { get; set; }
        public bool IsDeleted { get; set; }
    }
}
