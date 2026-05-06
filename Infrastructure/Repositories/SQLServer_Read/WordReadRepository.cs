using Application.Abstractions.Repositories.Read;
using Application.Models.Word;
using AutoMapper;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public sealed class WordReadRepository : IWordReadRepository
    {
        private readonly ApplicationDbReadContext _dbContext;
        private readonly IMapper _mapper;

        public WordReadRepository(ApplicationDbReadContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task DeleteAsync(Guid Id, DateTime ActionAt)
        {
            var entity = await _dbContext.Words
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(t => t.Id == Id);
            if (entity != null)
            {
                if (entity.UpdatedAt >= ActionAt)
                {
                    return;
                }
                entity.IsDeleted = true;
                entity.UpdatedAt = ActionAt;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Domain.Entity.Word>> GetWordMeanigsByTextAsync(string text)
        {
            var word = await _dbContext.Words.Where(t => t.Text == text).ToListAsync();
            return _mapper.Map<IEnumerable<Domain.Entity.Word>>(word);
        }

        public async Task ToggleWordMemorization(Guid Id, bool isMemorized, DateTime ActionAt)
        {
            var existingWord = await _dbContext.Words.FindAsync(Id);
            if (existingWord != null)
            {
                if (existingWord.UpdatedAt >= ActionAt)
                {
                    return;
                }
                existingWord.IsMemorized = isMemorized;
                existingWord.UpdatedAt = ActionAt;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpsertAsync(WordReadModel word)
        {
                var existingWord = await _dbContext.Words.IgnoreQueryFilters().FirstOrDefaultAsync(w => w.Id == word.Id);
                if (existingWord != null)
                {
                    if (existingWord.UpdatedAt >= word.UpdatedAt)
                    {
                        return;
                    }
                    _mapper.Map(word, existingWord);
                    existingWord.IsDeleted = false;
                }
                else
                {
                    var wordDb = _mapper.Map<Word>(word);
                    wordDb.IsDeleted = false;
                    _dbContext.Words.Add(wordDb);
                }
                await _dbContext.SaveChangesAsync();
        }
    }
}
