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
    public sealed class GetCourseCommentQueryHandler : IQueryHandler<GetCourseCommentQuery, PaginationResponse<CommentResponse>>
    {
        private readonly ICommentReadRepository _commentReadRepository;
        public GetCourseCommentQueryHandler(ICommentReadRepository commentReadRepository)
        {
            _commentReadRepository = commentReadRepository;
        }
        public async Task<PaginationResponse<CommentResponse>> Handle(GetCourseCommentQuery request, CancellationToken cancellationToken)
        {
            return await _commentReadRepository.GetByCourseIdAsync(request.CourseId, request.PageIndex, request.PageSize, cancellationToken);
        }
    }
}
