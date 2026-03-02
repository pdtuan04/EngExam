using Application.Repositories;
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
    public class ExamCategoryRepository : GenericRepository<Domain.Entity.ExamCategory, ExamCategory>,IExamCategoryRepository
    {
        public ExamCategoryRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ICollection<Domain.Entity.ExamCategory>> GetAllAsync()
        {
            var result = await _dbContext.ExamCategories.ToListAsync();
            return _mapper.Map<ICollection<Domain.Entity.ExamCategory>>(result);
        }
    }
}
