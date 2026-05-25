using Application.Abstractions.Repositories.Read;
using Application.Models.Comment;
using Application.Models.Exam;
using Application.Models.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Common;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.EntityFrameworkCore;
using OpenAI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public sealed class CommentReadRepository : ICommentReadRepository
    {
        private readonly ApplicationDbReadContext _dbContext;
        private readonly IMapper _mapper;

        public CommentReadRepository(ApplicationDbReadContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task DeleteAsync(Guid id, DateTime DeletedAt)
        {
            var comment = await _dbContext.Comments
                                        .IgnoreQueryFilters()
                                        .AsTracking()
                                        .FirstOrDefaultAsync(t => t.Id == id);
            if (comment != null)
            {
                if (comment.UpdatedAt >= DeletedAt)
                {
                    return;
                }
                comment.IsDeleted = true;
                comment.UpdatedAt = DeletedAt;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<PaginationResponse<CommentResponse>> GetByCourseIdAsync(Guid courseId, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var rootCommentQuery = _dbContext.Comments.Where(c => c.CourseId == courseId && c.ParentId == null);
            var totalCount = await rootCommentQuery.CountAsync(cancellationToken);
            if(totalCount == 0)
            {
                return new PaginationResponse<CommentResponse>(new List<CommentResponse>(), 0 , pageIndex, pageSize );
            }
            var rootComments = await rootCommentQuery.OrderByDescending(c => c.CreatedAt).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            var rootCommentIds = rootComments.Select(c => c.Id).ToList();
            var replyCount = await _dbContext.Comments.Where(c => rootCommentIds.Contains(c.RootCommentId) && c.ParentId != null).GroupBy(c => c.RootCommentId).Select(g => new
            {
                RootCommentId = g.Key,
                Count = g.Count()
            }).ToDictionaryAsync(x => x.RootCommentId, x => x.Count, cancellationToken);
            var commentResponses = rootComments.Select(c => new CommentResponse(c.Id, c.CourseId, c.UserId,c.UserName, c.UserAvatarUrl, c.Content, c.ParentId, c.RootCommentId, c.Path, c.Level, c.IsDeleted, replyCount.GetValueOrDefault(c.Id))).ToList();
            return new PaginationResponse<CommentResponse>(commentResponses, totalCount, pageIndex, pageSize);
        }

        public async Task<PaginationResponse<CommentResponse>> GetCommentReplyAsync(Guid rootId, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var commentQuery = _dbContext.Comments.Where(c => c.RootCommentId == rootId && c.ParentId != null);

            var totalCount = await commentQuery.CountAsync(cancellationToken);

            if (totalCount == 0)
            {
                return new PaginationResponse<CommentResponse>(new List<CommentResponse>(), 0, pageIndex, pageSize);
            }
            var comments = await commentQuery.OrderBy(c => c.Path)
                                            .Skip((pageIndex - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToListAsync(cancellationToken);
            var commentIds = comments.Select(c => c.Id).ToList();
            var replyCount = await _dbContext.Comments
                                                    .Where(c => commentIds.Contains(c.ParentId.Value))
                                                    .GroupBy(c => c.ParentId)
                                                    .Select(g => new
                                                    {
                                                        ParentId = g.Key,
                                                        Count = g.Count()
                                                    })
                                                    .ToDictionaryAsync(x => x.ParentId.Value, x => x.Count, cancellationToken);
            var commentResponses = comments.Select(c => new CommentResponse(
                c.Id,
                c.CourseId,
                c.UserId,
                c.UserName,
                c.UserAvatarUrl,
                c.Content,
                c.ParentId,
                c.RootCommentId,
                c.Path,
                c.Level,
                c.IsDeleted,
                replyCount.GetValueOrDefault(c.Id)
            )).ToList();

            return new PaginationResponse<CommentResponse>(commentResponses, totalCount, pageIndex, pageSize);
        }

        public async Task UpsertAsync(CommentReadModel comment)
        {
            var existingComment = await _dbContext.Comments.IgnoreQueryFilters().AsTracking().FirstOrDefaultAsync(t => t.Id == comment.Id);
            if (existingComment != null)
            {
                if (existingComment.UpdatedAt >= comment.UpdatedAt)
                {
                    return;
                }
                _mapper.Map(comment, existingComment);
            }
            else
            {
                var newComment = _mapper.Map<Comment>(comment);
                newComment.IsDeleted = false;
                _dbContext.Comments.Add(newComment);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
