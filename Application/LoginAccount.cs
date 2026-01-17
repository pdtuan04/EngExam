using Application.DTOs.Responses;
using Application.Exceptions;
using Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class LoginAccount : ILoginAccount
    {
        private readonly IAuthService _authService;
        public LoginAccount(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<LoginResponse> LoginAccount_Default(DTOs.Requests.Account.LoginAccountDefaultRequest user)
        {
            var result = await _authService.Login(user.UserName, user.Password, user.RememberMe);
            return result;
        }
    }
}
