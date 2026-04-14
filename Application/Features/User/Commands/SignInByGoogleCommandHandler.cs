using Application.Abstractions.Messaging;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Models.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands
{
    public sealed class SignInByGoogleCommandHandler : ICommandHandler<SignInByGoogleCommand, SignInResponse>
    {
        private readonly IAuthIdentityService _authIdentityService;
        public SignInByGoogleCommandHandler(IAuthIdentityService authIdentityService)
        {
            _authIdentityService = authIdentityService;
        }
        public async Task<SignInResponse> Handle(SignInByGoogleCommand request, CancellationToken cancellationToken)
        {
            return await _authIdentityService.LoginByGoogle(request.idToken) ?? throw new BadRequestException("Login unsuccess");
        }
    }
}
