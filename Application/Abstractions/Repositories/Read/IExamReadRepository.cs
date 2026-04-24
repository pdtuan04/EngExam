using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Models;
using Domain.Entity;

namespace Application.Abstractions.Repositories.Read
{
    public interface IExamReadRepository : IGenericReadRepository<Exam>
    {
        Task<IEnumerable<Exam>> GetAllAsync();
        Task<Exam> GetRandomExam();
        Task<IEnumerable<Exam>> GetExamsByCategoryIdAsync(Guid id);
        Task<Exam> GetExamToTake(Guid id);
        Task<Exam> GetExamDetail(Guid id);
        Task UpsertAsync(Exam exam);
    }
}
