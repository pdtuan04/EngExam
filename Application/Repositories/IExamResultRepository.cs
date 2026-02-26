using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Application.Repositories
{
    public interface IExamResultRepository : IGenericRepository<ExamResult>
    {
        Task AddAsync(ExamResult examResult);
        Task<ExamResult?> GetByIdAsync(Guid id);
        Task<IEnumerable<ExamResult>> GetResultsByUserId(Guid id);
    }
}
