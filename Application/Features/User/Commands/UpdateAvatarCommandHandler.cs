using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Common.Interfaces;
using Application.Features.User.Events;
using Application.Models.User;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands
{
    public sealed class UpdateAvatarCommandHandler : ICommandHandler<UpdateAvatarCommand, UserDetailResponse>
    {
        private readonly IAuthIdentityService _authIdentityService;
        private readonly IEventBus _eventBus;
        public UpdateAvatarCommandHandler(IAuthIdentityService authIdentityService, IEventBus eventBus)
        {
            _authIdentityService = authIdentityService;
            _eventBus = eventBus;
        }
        public async Task<UserDetailResponse> Handle(UpdateAvatarCommand request, CancellationToken cancellationToken)
        { 
            var user = await _authIdentityService.ChangeAvatar(request.UserId, request.AvatarUrl);
            await _eventBus.PublishAsync(new UserAvatarUpdatedEvent(user.Id, user.ImageUrl, user.UpdatedAt), cancellationToken);
            return new UserDetailResponse(user.Id, user.UserName, user.Email, user.ImageUrl, user.Age);
        }
    }
}
