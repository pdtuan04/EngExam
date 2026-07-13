using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class PasswordMismatchException : BusinessException
    {
        public PasswordMismatchException(string message = "Current password is incorrect.") : base(message, 400) {}
    }
}
