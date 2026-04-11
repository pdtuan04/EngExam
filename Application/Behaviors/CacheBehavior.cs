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
    public sealed class CacheBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class, ICacheQuery<TResponse>
    {
        private readonly ICacheService _cacheService;
        public CacheBehavior(ICacheService cacheService) 
        {
            _cacheService = cacheService ?? throw new ArgumentNullException();
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var cache = _cacheService.GetOrCreateAsync(
                request.CacheKey,
                _ => next(),
                request.Expiration,
                cancellationToken
            );   
            return await next();
        }
    }   
}
