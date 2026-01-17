using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Entity;

namespace Application.Repositories
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetAllAsync();
        Task<Question> GetByIdAsync(Guid id);
        Task<IEnumerable<Question>> GetByIdExamAsync(Guid id);
        Task AddAsync(Question question);
        Task UpdateAsync(Question question);
        Task<bool> DeleteAsync(Guid id);
    }
}
