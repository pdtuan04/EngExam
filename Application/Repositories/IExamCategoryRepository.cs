using Application.Models.ExamCategory;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IExamCategoryRepository : IGenericRepository<ExamCategory>
    {
        Task<ICollection<ExamCategory>> GetAllAsync();
        Task SoftDelete(Guid id);
    }
}
