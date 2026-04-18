using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using AutoMapper;
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

        public async Task<ICollection<Domain.Entity.ExamCategory>> GetAllAsync()
        {
            var result = await _dbContext.ExamCategories.Where(e => e.IsActive == true).ToListAsync();
            return _mapper.Map<ICollection<Domain.Entity.ExamCategory>>(result);
        }
    }
}
