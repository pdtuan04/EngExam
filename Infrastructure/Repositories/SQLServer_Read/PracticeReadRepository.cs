using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Models.ExamResult;
using Application.Models.Pagination;
using Application.Models.Practice;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore.BulkExtensions;
using Infrastructure.Common;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public class PracticeReadRepository : IPracticeReadRepository
    {
        private readonly ApplicationDbReadContext _dbContext;
        private readonly IMapper _mapper;

        public PracticeReadRepository(ApplicationDbReadContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }


        public async Task<PaginationResponse<PracticeResponse>> GetPracticePaginatedByTopicIdAsync(Guid topicId, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var query = _dbContext.Practices
                .AsNoTracking()
                .Where(p => p.TopicId == topicId);
            var projectedQuery = query.ProjectTo<PracticeResponse>(_mapper.ConfigurationProvider);
            var queryExecute = await PaginationDb<PracticeResponse>.ToPagedList(projectedQuery, pageIndex, pageSize);
            return new PaginationResponse<PracticeResponse>(queryExecute.Items, queryExecute.TotalCount, pageIndex, pageSize);
        }

        public async Task<Domain.Entity.Practice> GetPracticeToTake(Guid id)
        {
            var practice = await _dbContext.Practices
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<Domain.Entity.Practice>(practice);
        }

        public async Task UpsertAsync(PracticeReadModel practice)
        {
            var existingPractice = await _dbContext.Practices.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == practice.Id);
            if (existingPractice != null)
            {
                if (existingPractice.UpdatedAt >= practice.UpdatedAt)
                {
                    return;
                }
                _mapper.Map(practice, existingPractice);
            }
            else
            {
                var newPractice = _mapper.Map<Practice>(practice);
                newPractice.IsDeleted = false;
                _dbContext.Practices.Add(newPractice);
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid practiceId, DateTime deletedAt)
        {
            var practice = await _dbContext.Practices
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(p => p.Id == practiceId);
            if (practice != null)
            {
                if (practice.UpdatedAt >= deletedAt)
                {
                    return;
                }
                practice.IsDeleted = true;
                practice.UpdatedAt = deletedAt;
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
