using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Models.ExamCategory;
using Application.Models.Pagination;
using Application.Models.Topic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore.BulkExtensions;
using Infrastructure.Common;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public class ExamCategoryReadRepository : IExamCategoryReadRepository
    {
        private readonly ApplicationDbReadContext _dbContext;
        private readonly IMapper _mapper;

        public ExamCategoryReadRepository(ApplicationDbReadContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task DeleteAsync(Guid categoryId, DateTime deletedAt)
        {
            var examCategory = await _dbContext.ExamCategories
            .IgnoreQueryFilters()
            .AsTracking()
            .FirstOrDefaultAsync(t => t.Id == categoryId);
            if (examCategory != null)
            {
                if (examCategory.UpdatedAt >= deletedAt)
                {
                    return;
                }
                examCategory.IsDeleted = true;
                examCategory.UpdatedAt = deletedAt;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<ICollection<ExamCategoryResponse>> GetAllAsync()
        {
            var result = await _dbContext.ExamCategories.Where(e => e.IsActive == true).ToListAsync();
            return _mapper.Map<ICollection<ExamCategoryResponse>>(result);
        }

        public async Task<ExamCategoryResponse> GetByIdAsync(Guid id)
        {
            var dbExamCategory = await _dbContext.ExamCategories.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<ExamCategoryResponse>(dbExamCategory);
        }

        public async Task<PaginationResponse<ExamCategoryResponse>> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var query = _dbContext.ExamCategories.AsNoTracking();
            var projectedQuery = query.ProjectTo<ExamCategoryResponse>(_mapper.ConfigurationProvider);
            var queryExecute = await PaginationDb<ExamCategoryResponse>.ToPagedList(projectedQuery, page, pageSize);
            return new PaginationResponse<ExamCategoryResponse>(queryExecute.Items, queryExecute.TotalCount, page, pageSize);
        }

        public async Task UpsertAsync(ExamCategoryReadModel examCategory)
        {
            var existingExamCategory = await _dbContext.ExamCategories.IgnoreQueryFilters().AsTracking().FirstOrDefaultAsync(t => t.Id == examCategory.Id);
            if (existingExamCategory != null)
            {
                if (existingExamCategory.UpdatedAt >= examCategory.UpdatedAt)
                {
                    return;
                }
                _mapper.Map(examCategory, existingExamCategory);
            }
            else
            {
                var newExamCategory = _mapper.Map<ExamCategory>(examCategory);
                newExamCategory.IsDeleted = false;
                _dbContext.ExamCategories.Add(newExamCategory);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
