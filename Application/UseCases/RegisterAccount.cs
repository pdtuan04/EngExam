using Application.DTOs.Requests.Account;
using Application.Exceptions;
using Application.Interface.Identity;
using Domain.Entity;

namespace Application.UseCases
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
            if (!string.Equals(request.Password, request.ConfirmPassword)) throw new Exception("");
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
            await _roleServices.AddUserToRole(user, "User");
            if (!resutl) throw new AccountRegisterFailedException("Đăng ký không thành công");
            
            return resutl;
        }
    }
}
