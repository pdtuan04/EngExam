using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Events
{
    public sealed record CreateUserEvent(Guid Id, string UserName, string Email, int? Age, DateTime CreatedAt, DateTime UpdatedAt);
}
