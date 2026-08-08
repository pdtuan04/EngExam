using Application.Abstractions.Repositories.Read;
using Application.Models.Vocabulary;
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
    public sealed class VocabularyReadRepository : IVocabularyReadRepository
    {
        private readonly ApplicationDbReadContext _dbContext;
        private readonly IMapper _mapper;
        public VocabularyReadRepository(ApplicationDbReadContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IList<VocabularyResponse>> GetRandomWordsAsync(int count, CancellationToken cancellationToken)
        {
            var vocabularies = await _dbContext.Vocabularies.Take(count).ToListAsync(cancellationToken);
            return vocabularies.Select(v => _mapper.Map<VocabularyResponse>(v)).ToList();
        }
    }
}
