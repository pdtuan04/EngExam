using Application.Models.ExamCategory;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories.Read
{
    public interface IExamCategoryReadRepository : IGenericReadRepository<ExamCategory>
    {
        Task<ICollection<ExamCategory>> GetAllAsync();
        Task UpsertAsync(ExamCategory examCategory);
        Task DeleteAsync(Guid categoryId);
    }
}
