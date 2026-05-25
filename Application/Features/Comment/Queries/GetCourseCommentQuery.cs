using Application.Abstractions.Messaging;
using Application.Common.Caching;
using Application.Models.Comment;
using Application.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Queries
{
    public sealed record GetCourseCommentQuery(Guid CourseId, int PageIndex, int PageSize) : ICacheQuery<PaginationResponse<CommentResponse>>
    {
        public string CacheKey => CacheKeys.CourseComments(CourseId);

        public TimeSpan? Expiration => null;
    }
}
