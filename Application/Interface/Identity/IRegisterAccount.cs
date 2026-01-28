using Application.DTOs.Requests.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Identity
{
    public interface IRegisterAccount
    {
        public Task<bool> RegisterAccount_Default(RegisterAccountDefaultRequest request);
    }
}
