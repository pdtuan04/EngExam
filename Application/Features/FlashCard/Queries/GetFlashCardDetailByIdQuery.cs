using Application.Abstractions.Messaging;
using Application.Common.Caching;
using Application.Models.FlashCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FlashCard.Queries
{
    public record GetFlashCardDetailByIdQuery(Guid FlashCardId) : ICacheQuery<FlashCardDetailResponse>
    {
        public string CacheKey => CacheKeys.FlashCardDetail(FlashCardId);

        public TimeSpan? Expiration => null;
    }
}
