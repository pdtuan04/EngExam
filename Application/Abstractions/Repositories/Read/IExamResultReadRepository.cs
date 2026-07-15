using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.ExamResult;
using Application.Models.Pagination;
using Domain.Entity;

namespace Application.Abstractions.Repositories.Read
{
    public interface IExamResultReadRepository
    {
        Task<ExamResultDetailResponse?> GetByIdAsync(Guid id);
        Task<IEnumerable<ExamResultResponse>> GetResultsByUserId(Guid id);
        Task<PaginationResponse<ExamResultResponse>> GetExamResultPaginatedByUserId(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task<ExamResultDetailResponse> GetDetailByIdAsync(Guid id);
        Task UpsertAsync(ExamResultReadModel examResultReadModel);
        Task<int> GetCompletedExamCountByMonthAsync(int year, int month, CancellationToken cancellationToken);
        Task<int> GetCompletedExamCountByYearAsync(int year, CancellationToken cancellationToken);
    }
}
