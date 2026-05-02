using Application.Abstractions.Messaging;
using Application.Common.Caching;
using Application.Models.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Topic.Queries
{
    public sealed record GetAllTopicQuery() : ICacheQuery<IEnumerable<TopicResponse>>
    {
        public string CacheKey => CacheKeys.AllTopics;
        public TimeSpan? Expiration => null;
    }
}
