using Application.Abstractions.Messaging;
using Application.Common.Caching;
using Application.Models.FlashCard;

namespace Application.Features.FlashCard.Queries
{
    public sealed record GetFlashCardByUserIdQuery(Guid UserId) : ICacheQuery<IEnumerable<FlashCardResponse>>
    {
        public string CacheKey => CacheKeys.FlashCardsByUser(UserId);

        public TimeSpan? Expiration => null;
    }
}
