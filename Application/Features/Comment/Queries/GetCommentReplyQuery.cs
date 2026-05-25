using Application.Abstractions.Messaging;
using Application.Models.Comment;
using Application.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Queries
{
    public sealed record GetCommentReplyQuery(Guid ParentId, int PageIndex, int PageSize) : IQuery<PaginationResponse<CommentResponse>>;
}
