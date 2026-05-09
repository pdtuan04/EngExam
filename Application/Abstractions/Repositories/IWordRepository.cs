using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories
{
    public interface IWordRepository : IGenericRepository<Word>
    {
        Task<Word> GetByTextAsync(string text);
        Task<bool> ToggleMemorizedStatusAsync(Guid wordId, bool isMemorized, DateTime updatedAt);
    }
}
