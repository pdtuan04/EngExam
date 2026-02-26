using Application.Models.Authen;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAuthIdentityService
    {
        Task<bool> CheckUserExist(string userName, string email);
        Task<bool> SignUp(User request);
        Task<SignInResponse> SignIn(string username, string password, bool rememberme);
        Task<string> JwtTokenGen(User user);
        Task<bool> ChangePassword(Guid userId, string currentPassword, string newPassword);
        Task<SignInResponse> LoginByGoogle(string idToken);
        Task<string> GetRole(string roleName);
        Task<string> CreateRole(string roleName);
        Task<bool> AddUserToRole(User user, string roleName);
        Task Logout();
    }
}
