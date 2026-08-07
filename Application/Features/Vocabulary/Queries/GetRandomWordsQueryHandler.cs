using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Models.Vocabulary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Vocabulary.Queries
{
    public sealed class GetRandomWordsQueryHandler : IQueryHandler<GetRandomWordsQuery, List<VocabularyResponse>>
    {
        private readonly IVocabularyReadRepository _vocabularyReadRepository;
        public GetRandomWordsQueryHandler(IVocabularyReadRepository vocabularyReadRepository)
        {
            _vocabularyReadRepository = vocabularyReadRepository;
        }
        public async Task<List<VocabularyResponse>> Handle(GetRandomWordsQuery request, CancellationToken cancellationToken)
        {
            var randomWords = await _vocabularyReadRepository.GetRandomWordsAsync(request.quantity, cancellationToken);
            return randomWords.ToList();
        }
    }
}