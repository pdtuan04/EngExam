using Application.Models.Exam;
using Application.Models.ExamCategory;
using Application.Models.Pagination;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories.Read
{
    public interface IExamCategoryReadRepository
    {
        Task<ExamCategoryResponse> GetByIdAsync(Guid id);
        Task<ICollection<ExamCategoryResponse>> GetAllAsync();
        Task UpsertAsync(ExamCategoryReadModel examCategory);
        Task DeleteAsync(Guid categoryId, DateTime deletedAt);
        Task<PaginationResponse<ExamCategoryResponse>> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken);
    }
}
