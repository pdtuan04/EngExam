using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Common.Constants;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.User.Events;
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
        private readonly IEventBus _eventBus;

        public SignInByGoogleCommandHandler(IAuthIdentityService authIdentityService, IEventBus eventBus)
        {
            _authIdentityService = authIdentityService;
            _eventBus = eventBus;
        }
        public async Task<SignInResponse> Handle(SignInByGoogleCommand request, CancellationToken cancellationToken)
        { 
            var payload = await _authIdentityService.LoginByGoogle(request.idToken) ?? throw new BadRequestException("Login unsuccess");
            var user = await _authIdentityService.GetUserByEmail(payload.Email);
            if (user == null)
            {
                var now = DateTime.UtcNow;
                var userId = Guid.CreateVersion7();
                var newUser = new Domain.Entity.User
                {
                    Id = userId,
                    UserName = payload.Email,
                    Email = payload.Email,
                    CreatedAt = now,
                    UpdatedAt = now
                };
                if (!await _authIdentityService.ExternalSignUp(newUser))
                    throw new BadRequestException("Failed to sign up user");
                await _authIdentityService.CreateRole(Roles.User);
                await _authIdentityService.AddUserToRole(newUser, Roles.User);
                await _eventBus.PublishAsync(new UserCreatedEvent(newUser.Id, newUser.UserName, payload.Email, null, new[] { Roles.User }, newUser.CreatedAt, newUser.UpdatedAt), cancellationToken);
                var token = await _authIdentityService.JwtTokenGen(newUser);
                var response = new SignInResponse(token, newUser.Id, newUser.UserName ?? "", newUser.Email ?? "", new List<string> { Roles.User }, newUser.ImageUrl);
                return response;
            }
            else
            {
                var userRoles = await _authIdentityService.GetUserRolesAsync(user);
                var token = await _authIdentityService.JwtTokenGen(user);
                var response = new SignInResponse(token, user.Id, user.UserName ?? "", user.Email ?? "", userRoles.ToList(), user.ImageUrl);
                return response;
            }
        }
    }
}
