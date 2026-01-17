using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests.Account
{
    public class LoginAccountDefaultRequest
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }

        public required bool RememberMe { get; set; } = false;
    }
}
