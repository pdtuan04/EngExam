using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Authen
{
    public sealed record SignInResponse(string Token, Guid UserId, string UserName, string Email, List<string> Role);
}
