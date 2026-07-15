using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Events
{
    public sealed record UserCreatedEvent(Guid Id, string UserName, string Email, int? Age, IEnumerable<string> Roles, DateTime CreatedAt, DateTime UpdatedAt);
}
