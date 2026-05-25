using Application.Models.Comment;
using Application.Models.Course;
using Application.Models.Pagination;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories.Read
{
    public interface ICommentReadRepository
    {
        Task UpsertAsync(CommentReadModel comment);
        Task DeleteAsync(Guid id, DateTime DeletedAt);
        Task<PaginationResponse<CommentResponse>> GetByCourseIdAsync(Guid id, int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task<PaginationResponse<CommentResponse>> GetCommentReplyAsync(Guid rootId, int pageIndex, int pageSize, CancellationToken cancellationToken);
    }
}
