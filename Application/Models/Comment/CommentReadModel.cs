using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Comment
{
    public record CommentReadModel(Guid Id, Guid CourseId, Guid UserId, string UserName, string UserAvatarUrl, string Content, Guid RootCommentId, string Path, int Level, DateTime CreatedAt, DateTime UpdatedAt, bool IsDeleted, Guid? ParentId);
}
