using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.ExamResult;
using Application.Models.Pagination;
using Domain.Entity;

namespace Application.Abstractions.Repositories
{
    public interface IExamResultRepository : IGenericRepository<ExamResult>
    {
        Task AddAsync(ExamResult examResult);
        Task<ExamResult?> GetByIdAsync(Guid id);
        Task<IEnumerable<ExamResult>> GetResultsByUserId(Guid id);
        Task<PaginationResponse<ExamResultResponse>> GetExamResultPaginatedByUserId(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task<ExamResult> GetDetailByIdAsync(Guid id);
    }
}
