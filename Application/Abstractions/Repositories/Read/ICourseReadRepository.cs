using Application.Models.Course;
using Application.Models.Pagination;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories.Read
{
    public interface ICourseReadRepository
    {
        Task<CourseDetailResponse?> GetByIdAsync(Guid courseId, CancellationToken cancellationToken);    
        Task<PaginationResponse<CourseResponse>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task UpsertAsync(CourseReadModel course);
        Task DeleteAsync(Guid id, DateTime DeletedAt);
    }
}
