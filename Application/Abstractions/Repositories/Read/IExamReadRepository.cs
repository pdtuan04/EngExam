using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Models;
using Application.Models.Exam;
using Application.Models.Pagination;
using Domain.Entity;

namespace Application.Abstractions.Repositories.Read
{
    public interface IExamReadRepository
    {
        Task<IEnumerable<ExamResponse>> GetAllAsync();
        Task<TakeExamResponse> GetRandomExam();
        Task<IEnumerable<ExamResponse>> GetExamsByCategoryIdAsync(Guid id);
        Task<TakeExamResponse> GetExamToTake(Guid id);
        Task<ExamDetailResponse> GetExamDetail(Guid id);
        Task<PaginationResponse<ExamResponse>> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task UpsertAsync(ExamReadModel exam);
        Task DeleteAsync(Guid id, DateTime deletedAt);
    }
}
