using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses
{
    public class LoginResponse
    {
        public required string Token { get; set; }
        public required string UserId { get; set; }
    }
}
