using Application.Abstractions.Messaging;
using Application.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Queries
{
    public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserDetailResponse>;
}
