using Application.Abstractions.Repositories.Read;
using AutoMapper;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public sealed class WordReadRepository : GenericReadRepository<Domain.Entity.Word, Word>, IWordReadRepository
    {
        public WordReadRepository(ApplicationDbReadContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
        public async Task<IEnumerable<Domain.Entity.Word>> GetWordMeanigsByTextAsync(string text)
        {
            var word = await _dbContext.Words.Where(t => t.Text == text).ToListAsync();
            return _mapper.Map<IEnumerable<Domain.Entity.Word>>(word);
        }
    }
}
