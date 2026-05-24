using Application.Abstractions.Repositories;
using AutoMapper;
using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer
{
    public sealed class CommentRepository : GenericRepository<Domain.Entity.Comment, Comment, Guid>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IEnumerable<Domain.Entity.Comment>> GetRootCommentDetailsById(Guid rootId)
        {
            var comments = await _dbContext.Comments
                .Where(c => c.RootCommentId == rootId && !c.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
            return comments;
        }
    }
}
