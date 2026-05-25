using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Events
{
    public record DeleteCommentEvent(Guid Id,Guid? ParentId,Guid CourseId, DateTime DeletedAt);
}
