using Application.Models.Pagination;
using Application.Models.Practice;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories
{
    public interface IPracticeRepository : IGenericRepository<Practice>
    {
        Task<Practice> GetPracticeToTake(Guid id);
        Task<PaginationResponse<PracticeResponse>> GetPracticePaginatedByTopicIdAsync(Guid topicId, int pageIndex, int pageSize, CancellationToken cancellationToken);
    }
}
