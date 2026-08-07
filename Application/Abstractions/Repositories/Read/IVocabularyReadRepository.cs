using Application.Models.Vocabulary;
using Domain.Entity;

namespace Application.Abstractions.Repositories.Read
{
    public interface IVocabularyReadRepository
    {
        Task<IList<VocabularyResponse>> GetRandomWordsAsync(int count, CancellationToken cancellationToken);
    }
}
