using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Exceptions;
using Application.Models.Authen;
using Application.Repositories;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthenService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthIdentityService _authIdentityService;
        public AuthenService(IUnitOfWork unitOfWork, IAuthIdentityService authIdentityService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _authIdentityService = authIdentityService;
        }

        public async Task<SignInResponse> SignIn(string username, string password, bool rememberme)
        {
            return await _authIdentityService.SignIn(username, password, rememberme) ?? throw new BadRequestException("Invalid username or password");
        }

        public async Task<bool> SignUp(SignUpRequest request)
        {
            if (!string.Equals(request.Password, request.ConfirmPassword)) throw new BadRequestException("Password and ConfirmPassword not match");
            if (await _authIdentityService.CheckUserExist(request.UserName, request.Password) == true) throw new AccountRegisterFailedException();

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Password = request.Password,
                Age = request.Age,
                Email = request.Email
            };
            var resutl = await _authIdentityService.SignUp(user);
            if (!resutl) throw new AccountRegisterFailedException("Sign up unsuccess");
            await _authIdentityService.CreateRole("User");
            await _authIdentityService.AddUserToRole(user, "User");
            return resutl;
        }
        public async Task<SignInResponse> LoginByGoogle(string idToken)
        {
            return await _authIdentityService.LoginByGoogle(idToken) ?? throw new BadRequestException("Login unsuccess");
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ChangeUserPassword(ChangePasswordRequest request)
        {
            return await _authIdentityService.ChangePassword(request.UserId, request.CurrentPassword, request.NewPassword);
        }
    }
}
