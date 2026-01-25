using Application.DTOs.Requests.Account;
using Application.Interface;
using Application.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class ChangePassword : IChangePasswordManager
    {
        private readonly IAuthService _authService;
        public ChangePassword(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<bool> ChangeUserPassword(ChangePasswordDTO request)
        {
            await _authService.ChangePasswordAsync(request.UserId, request.CurrentPassword, request.NewPassword);
            return true;
        }
    }
}
