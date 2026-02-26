using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Models;
using Domain.Entity;

namespace Application.Repositories
{
    public interface IExamRepository : IGenericRepository<Exam>
    {
        Task<IEnumerable<Exam>> GetAllAsync();
        Task<Exam> GetRandomExam();
        Task<IEnumerable<Exam>> GetExamsByCategoryIdAsync(Guid id);
        Task<Guid> AddAsync(Exam exam);
    }
}
