using Application.DTOs.Requests.Account;
using Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ILoginAccount
    {
        public Task<LoginResponse> LoginAccount_Default(LoginAccountDefaultRequest user);
    }
}
