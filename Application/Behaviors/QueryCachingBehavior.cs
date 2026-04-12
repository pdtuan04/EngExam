using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behaviors
{
    public sealed class QueryCachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheQuery
    {
        private readonly ICacheService _cacheService;
        public QueryCachingBehavior(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            return await _cacheService.GetOrCreateAsync(request.CacheKey,_ => next(), request.Expiration, cancellationToken);
        }
    }
}
