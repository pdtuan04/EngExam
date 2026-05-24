using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Comment
{
    public record CommentResponse(Guid id, Guid courseId, Guid userId, string userAvatarUrl, string content, Guid? parentId, Guid rootCommentId, string path, int level, bool isDeleted);
}
