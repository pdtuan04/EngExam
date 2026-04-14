using Application.Abstractions.Messaging;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands
{
    public sealed class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand, bool>
    {
        private readonly IAuthIdentityService _authIdentityService;
        public ChangePasswordCommandHandler(IAuthIdentityService authIdentityService)
        {
            _authIdentityService = authIdentityService;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            return await _authIdentityService.ChangePassword(request.UserId, request.CurrentPassword, request.NewPassword);
        }
    }
}
