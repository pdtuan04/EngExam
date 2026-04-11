using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Caching
{
    public interface ICacheService
    {
        Task<T> GetOrCreateAsync<T>(
            string cacheKey,
            Func<CancellationToken,Task<T>> factory,
            TimeSpan? expiration = null,
            CancellationToken cancellationToken = default
        );
    }
}
