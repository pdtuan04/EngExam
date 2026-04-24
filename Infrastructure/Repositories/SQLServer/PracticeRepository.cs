using Application.Abstractions.Repositories;
using Application.Models.Pagination;
using Application.Models.Practice;
using AutoMapper;
using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer
{
    public class PracticeRepository : GenericRepository<Domain.Entity.Practice, Practice>, IPracticeRepository
    {
        public PracticeRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper) 
        {
        }

        public async Task<PaginationResponse<PracticeResponse>> GetPracticePaginatedByTopicIdAsync(Guid topicId, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            Expression<Func<Domain.Entity.Practice, bool>> filter = p => p.TopicId == topicId;

            return await ToPagination<PracticeResponse>(pageIndex, pageSize, filter, cancellationToken: cancellationToken);
        }

        public async Task<Domain.Entity.Practice> GetPracticeDetail(Guid id)
        {
            var practice = await _dbContext.Practices
                .AsNoTracking()
                .Include(p => p.PracticeDetails)
                    .ThenInclude(pd => pd.Question)
                        .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<Domain.Entity.Practice>(practice);
        }
    }
}
