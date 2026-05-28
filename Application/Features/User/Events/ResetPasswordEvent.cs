using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Events
{
    public sealed record ResetPasswordEvent(string Email, DateTime Timestamp);
}
