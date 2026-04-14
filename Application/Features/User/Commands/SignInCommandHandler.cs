using Application.Abstractions.Messaging;
using Application.Common.Interfaces;
using Application.Models.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands
{
    public sealed class SignInCommandHandler : ICommandHandler<SignInCommand, SignInResponse>
    {
        private readonly IAuthIdentityService _authIdentityService;
        public SignInCommandHandler(IAuthIdentityService authIdentityService)
        {
            _authIdentityService = authIdentityService;
        }
        public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            return await _authIdentityService.SignIn(request.username, request.password, request.rememberme);
        }
    }
}
