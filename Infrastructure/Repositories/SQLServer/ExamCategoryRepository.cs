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
    public class ExamCategoryRepository : IExamCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ExamCategoryRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<Domain.Entity.ExamCategory>> GetAllAsync()
        {
            var dbExamCategories = await _context.ExamCategories.ToListAsync();
            return _mapper.Map<IEnumerable<Domain.Entity.ExamCategory>>(dbExamCategories);
        }
    }
}
