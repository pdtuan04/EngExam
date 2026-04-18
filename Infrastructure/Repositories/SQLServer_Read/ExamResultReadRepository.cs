using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Models.ExamResult;
using Application.Models.Pagination;
using AutoMapper;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public class ExamResultReadRepository : GenericReadRepository<Domain.Entity.ExamResult, ExamResult> ,IExamResultReadRepository
    {
        public ExamResultReadRepository(ApplicationDbReadContext context, IMapper mapper) : base(context, mapper)
        {
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
        public async Task<PaginationResponse<ExamResultResponse>> GetExamResultPaginatedByUserId(Guid userId, int pageIndex, int pageSize,CancellationToken cancellationToken)
        {
            Expression<Func<Domain.Entity.ExamResult, bool>> filter = e => e.UserId == userId;
            return await ToPagination<ExamResultResponse>(pageIndex,pageSize,filter,cancellationToken: cancellationToken);

        }

        public async Task<Domain.Entity.ExamResult> GetDetailByIdAsync(Guid id)
        {
            var dbexamresult = await _dbContext.ExamResults
                .Include(er => er.AnswerHistory)
                .ThenInclude(ah => ah.Question)
                .ThenInclude(q => q.Answers)
                .AsNoTracking()
                .FirstOrDefaultAsync(er => er.Id == id);
            if (dbexamresult == null)
            {
                return null;
            }
            return _mapper.Map<Domain.Entity.ExamResult>(dbexamresult);
        }
    }
}
