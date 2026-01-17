using Application.DTOs.Requests.Account;
using Application.Exceptions;
using Application.Interface;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class RegisterAccount : IRegisterAccount
    {
        private readonly IAuthService _authService;
        private readonly IRoleServices _roleServices;
        public RegisterAccount(IAuthService authService, IRoleServices roleServices)
        {
            _authService = authService;
            _roleServices = roleServices;
        }
        public async Task<bool> RegisterAccount_Default(RegisterAccountDefaultRequest request)
        {
            if(await _authService.CheckUserExist(request.UserName, request.Password) == true) throw new AccountRegisterFailedException();

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Password = request.Password,
                Age = request.Age,
                Email = request.Email
            };
            var resutl = await _authService.Register(user);
            await _roleServices.CreateRole("User");
            await _authService.AddUserToRole(user, "User");
            if (!resutl) throw new AccountRegisterFailedException("Đăng ký không thành công");
            
            return resutl;
        }
    }
}
