using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Models.Comment;
using Application.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Queries
{
    public sealed class GetCommentReplyQueryHandler : IQueryHandler<GetCommentReplyQuery, PaginationResponse<CommentResponse>>
    {
        private readonly ICommentReadRepository _commentReadRepository;
        public GetCommentReplyQueryHandler(ICommentReadRepository commentReadRepository)
        {
            _commentReadRepository = commentReadRepository;
        }
        public async Task<PaginationResponse<CommentResponse>> Handle(GetCommentReplyQuery request, CancellationToken cancellationToken)
        {
            var commentReplies = await _commentReadRepository.GetCommentReplyAsync(request.ParentId, request.PageIndex, request.PageSize, cancellationToken);
            return commentReplies;
        }
    }
}
