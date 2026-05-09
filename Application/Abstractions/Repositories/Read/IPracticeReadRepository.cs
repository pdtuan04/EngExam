using Application.Models.Exam;
using Application.Models.Pagination;
using Application.Models.Practice;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories.Read
{
    public interface IPracticeReadRepository
    {
        Task<PracticeDetailResponse> GetPracticeToTake(Guid id);
        Task<PaginationResponse<PracticeResponse>> GetPracticePaginatedByTopicIdAsync(Guid topicId, int pageIndex, int pageSize, CancellationToken cancellationToken);
        Task UpsertAsync(PracticeReadModel practice);
        Task DeleteAsync(Guid practiceId, DateTime deletedAt);
        Task UpsertPracticeDetailsAsync(IEnumerable<PracticeDetailReadModel> details);
    }
}
