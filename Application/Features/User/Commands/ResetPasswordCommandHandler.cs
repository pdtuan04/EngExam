using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Common.Interfaces;
using Application.Features.User.Events;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands
{
    public sealed class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, bool>
    {
        private readonly IAuthIdentityService _authIdentityService;
        private readonly IEventBus _eventBus;
        public ResetPasswordCommandHandler(IAuthIdentityService authIdentityService, IEventBus eventBus)
        {
            _authIdentityService = authIdentityService;
            _eventBus = eventBus;
        }
        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var result = await _authIdentityService.ResetPassword(request.Email, Uri.UnescapeDataString(request.Token), request.NewPassword);
            await _eventBus.PublishAsync(new ResetPasswordEvent(request.Email, now));
            return result;
        }
    }
}
