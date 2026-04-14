using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Authen
{
    public sealed record SignInRequest(
    string UserName,
    string Password,
    bool RememberMe = false);
}
