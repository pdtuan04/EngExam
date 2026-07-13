using Application.Abstractions.Messaging;
using Application.Common.Caching;
using Application.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Queries
{
    public sealed record GetUserByIdQuery(Guid UserId) : ICacheQuery<UserDetailResponse>
    {
        public string CacheKey => CacheKeys.UserDetail(UserId);

        public TimeSpan? Expiration => null;
    }
}
