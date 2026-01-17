using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using AutoMapper;
using Infrastructure.Repositories.SQLServer.DataContext;

namespace Infrastructure.Repositories.SQLServer
{
    public class ExamResultRepository : IExamResultRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ExamResultRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task AddAsync(Domain.Entity.ExamResult examResult)
        {
            var dbexamresult = _mapper.Map<ExamResult>(examResult);
            await _context.ExamResults.AddAsync(dbexamresult);
            await _context.SaveChangesAsync();
        }
    }
}
