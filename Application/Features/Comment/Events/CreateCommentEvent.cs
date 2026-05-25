using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Events
{
    public record CreateCommentEvent(Guid Id, Guid CourseId, Guid UserId, string UserName, string UserAvatarUrl, string Content, Guid? ParentId, Guid RootCommentId, string Path, int Level, bool IsDeleted, DateTime CreatedAt);
}
