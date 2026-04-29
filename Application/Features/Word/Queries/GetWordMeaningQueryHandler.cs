using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Models.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Word.Queries
{
    public sealed class GetWordMeaningQueryHandler : IQueryHandler<GetWordMeaningQuery, IEnumerable<WordMeaningsResponse>>
    {
        private readonly IWordReadRepository _wordReadRepository;
        public GetWordMeaningQueryHandler(IWordReadRepository wordReadRepository)
        {
            _wordReadRepository = wordReadRepository;
        }
        public async Task<IEnumerable<WordMeaningsResponse>> Handle(GetWordMeaningQuery request, CancellationToken cancellationToken)
        {
            var words = await _wordReadRepository.GetWordMeanigsByTextAsync(request.Text);
            if (words == null || !words.Any()) return null;
            return words.Select(w => new WordMeaningsResponse(w.Id, w.Text, w.Meaning));
        }
    }
}
