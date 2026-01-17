using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class AccountLoginFailedException : BusinessException
    {
        public AccountLoginFailedException(string message = "Tên tài khoản hoặc mật khẩu không đúng") : base(message, 401){}
    }
}
