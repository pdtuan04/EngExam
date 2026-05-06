using Application.Models.Word;
using Domain.Entity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories.Read
{
    public interface IWordReadRepository
    {
        Task<IEnumerable<Word>> GetWordMeanigsByTextAsync(string text);
        Task UpsertAsync(WordReadModel word);
        Task DeleteAsync(Guid Id, DateTime ActionAt);
        Task ToggleWordMemorization(Guid Id, bool isMemorized, DateTime ActionAt);
    }
}
