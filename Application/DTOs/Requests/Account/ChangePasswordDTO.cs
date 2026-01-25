using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests.Account
{
    public class ChangePasswordDTO
    {
        public required Guid UserId { get; set; }
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
        public required string ConfirmNewPassword { get; set; }
    }
}
