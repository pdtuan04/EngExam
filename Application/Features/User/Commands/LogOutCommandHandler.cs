using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Application.Common.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands
{
    public sealed class LogOutCommandHandler : ICommandHandler<LogOutCommand, bool>
    {
        private readonly ICacheService _cacheService;
        public LogOutCommandHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task<bool> Handle(LogOutCommand request, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveCacheAsync(CacheKeys.JwtToken(request.Token));
            return true;
        }
    }
}
