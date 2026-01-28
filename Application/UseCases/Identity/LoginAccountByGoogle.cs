using Application.DTOs.Responses;
using Application.Interface.Identity;

namespace Application.UseCases.Identity
{
    public class LoginAccountByGoogle : ILoginAccountByGoogle
    {
        private readonly IAuthService _authService;
        public LoginAccountByGoogle(IAuthService authService)
        {
            _authService = authService;
        }
        public Task<LoginResponse> LoginByGoogle(string idToken)
        {
            return _authService.LoginByGoogleAsync(idToken);
        }
    }
}
