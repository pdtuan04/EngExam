using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using AutoMapper;
using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SQLServer
{
    public class ExamResultRepository : GenericRepository<Domain.Entity.ExamResult, ExamResult> ,IExamResultRepository
    {
        public ExamResultRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public async Task AddAsync(Domain.Entity.ExamResult examResult)
        {
            var dbexamresult = _mapper.Map<ExamResult>(examResult);
            await _dbContext.ExamResults.AddAsync(dbexamresult);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Domain.Entity.ExamResult?> GetByIdAsync(Guid id)
        {
            var dbexamresult = await _dbContext.ExamResults.FindAsync(id);
            return dbexamresult is null ? null : _mapper.Map<Domain.Entity.ExamResult>(dbexamresult);
        }
        public async Task<IEnumerable<Domain.Entity.ExamResult>> GetAllAsync()
        {
            var dbexamresults = await _dbContext.ExamResults.AsNoTracking().ToListAsync();
            return _mapper.Map<List<Domain.Entity.ExamResult>>(dbexamresults);
        }
        public async Task<IEnumerable<Domain.Entity.ExamResult>> GetResultsByUserId(Guid id)
        {
            var dbexamresults = await _dbContext.ExamResults
                .AsNoTracking()
                .Where(er => er.UserId == id)
                .ToListAsync();
            return _mapper.Map<IEnumerable<Domain.Entity.ExamResult>>(dbexamresults);
        }
    }
}
