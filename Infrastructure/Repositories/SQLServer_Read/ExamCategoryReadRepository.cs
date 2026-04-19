using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using AutoMapper;
using EFCore.BulkExtensions;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public class ExamCategoryReadRepository : GenericReadRepository<Domain.Entity.ExamCategory, ExamCategory>,IExamCategoryReadRepository
    {
        public ExamCategoryReadRepository(ApplicationDbReadContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task DeleteAsync(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Domain.Entity.ExamCategory>> GetAllAsync()
        {
            var result = await _dbContext.ExamCategories.Where(e => e.IsActive == true).ToListAsync();
            return _mapper.Map<ICollection<Domain.Entity.ExamCategory>>(result);
        }

        public async Task UpsertAsync(Domain.Entity.ExamCategory examCategory)
        {
            var dbExamCategory = _mapper.Map<ExamCategory>(examCategory);
            await _dbContext.BulkInsertOrUpdateAsync(new List<ExamCategory>
            {
                dbExamCategory
            });
        }
    }
}
