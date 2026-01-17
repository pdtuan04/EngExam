using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class AccountRegisterFailedException: BusinessException
    {
        public AccountRegisterFailedException(string message = "Tài khoản đã tồn tại."): base(message,409) { }
    }
}
