using Application.Abstractions.Messaging;
using Application.Common.Caching;
using Application.Models.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Word.Queries
{
    public sealed record GetWordMeaningQuery(string Text) : ICacheQuery<IEnumerable<WordMeaningsResponse>>
    {
        public string CacheKey => CacheKeys.WordMeaning(Text);

        public TimeSpan? Expiration => null;
    }
}
