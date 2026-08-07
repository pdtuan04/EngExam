using Application.Abstractions.Messaging;
using Application.Models.Vocabulary;

namespace Application.Features.Vocabulary.Queries
{
    public sealed record GetRandomWordsQuery(int quantity = 1) : IQuery<List<VocabularyResponse>>;
}
