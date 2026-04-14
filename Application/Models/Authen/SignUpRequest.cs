using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Authen
{
    public sealed record SignUpRequest(string UserName, string Email, string Password, string ConfirmPassword, int? Age);
}
