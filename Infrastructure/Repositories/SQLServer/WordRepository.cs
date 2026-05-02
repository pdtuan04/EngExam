using Application.Abstractions.Repositories;
using AutoMapper;
using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer
{
    public sealed class WordRepository : GenericRepository<Domain.Entity.Word, Word, Guid>, IWordRepository
    {
        public WordRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public async Task<Domain.Entity.Word> GetByTextAsync(string text)
        {
            var word = await _dbContext.Words.FirstOrDefaultAsync(t => t.Text == text);
            return _mapper.Map<Domain.Entity.Word>(word);
        }

        public async Task<bool> ToggleMemorizedStatusAsync(Guid wordId)
        {
            var word = await _dbContext.Words.FirstOrDefaultAsync(w => w.Id == wordId);
            word.IsMemorized = !word.IsMemorized;
            return word.IsMemorized;
        }
    }
}
