using Application.DTOs.Requests.Account;
using Application.DTOs.Responses;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Identity
{
    public interface IAuthService
    {
        public Task<LoginResponse> Login(string username, string password, bool rememberme);
        public Task<bool> Register(User request);
        public Task<bool> CheckUserExist(string username, string password);
        public Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
        public Task<LoginResponse> LoginByGoogleAsync(string idToken);
    }
}
