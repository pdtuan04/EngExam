using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entity;

namespace Application.Repositories
{
    public interface IExamRepository
    {
        Task<IEnumerable<Exam>> GetAllAsync();
        Task<Exam> GetRandomExam();
        Task<Exam> GetByIdAsync(Guid id);
        Task<IEnumerable<Exam>> GetExamsByCategoryIdAsync(Guid id);
        Task<Guid> AddAsync(Exam exam);
        Task<PaginatedList<Exam>> GetPaginatedExamAsync(string? search, string? sortBy, string sortDir, int pageNumber, int pageSize);
    }
}
