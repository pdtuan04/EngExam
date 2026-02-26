using Application.Models.Authen;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<SignInResponse> SignIn(string username, string password, bool rememberme);
        Task<bool> SignUp(SignUpRequest request);
        Task<SignInResponse> LoginByGoogle(string idToken);
        Task Logout();
        Task<bool> ChangeUserPassword(ChangePasswordRequest request);
    }
}
