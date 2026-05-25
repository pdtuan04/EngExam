using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Comment : BaseEntity<Guid>, ISoftDeletable
    {
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? ParentId { get; private set; }
        public Guid RootCommentId { get; private set; }
        public string Path { get; private set; }
        public int Level { get; private set; }
        protected Comment() { } 

        public static Comment CreateRoot(Guid courseId, Guid userId, string content)
        {
            var now = DateTime.UtcNow;
            var id = Guid.CreateVersion7();
            return new Comment
            {
                Id = id,
                CourseId = courseId,
                UserId = userId,
                Content = content,
                ParentId = null,
                RootCommentId = id,
                Path = $"{id}/",
                Level = 0,
                CreatedAt = now,
                UpdatedAt = now
            };
        }
        public Comment Reply(Guid userId, string content)
        {
            var now = DateTime.UtcNow;
            var replyId = Guid.CreateVersion7();
            return new Comment
            {
                Id = replyId,
                CourseId = this.CourseId,
                UserId = userId,
                Content = content,
                ParentId = this.Id,
                RootCommentId = this.RootCommentId,
                Path = $"{this.Path}{replyId}/",
                Level = this.Level + 1,
                CreatedAt = now,
                UpdatedAt = now
            };
        }
    }
}
