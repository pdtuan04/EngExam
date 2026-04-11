using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Messaging
{
    public interface ICacheQuery
    {
        string CacheKey { get; }
        TimeSpan? Expiration { get; }
    }
    public interface ICacheQuery<TResponse> : IQuery<TResponse>, ICacheQuery;
}
