using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Comment
{
    public record CreateCommentRequest(Guid parentId, string content, Guid courseId);
}
