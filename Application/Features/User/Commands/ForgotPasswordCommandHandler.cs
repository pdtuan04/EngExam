using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Common.Interfaces;
using Application.Features.User.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands
{
    public sealed class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand, bool>
    {
        private readonly IAuthIdentityService _authIdentityService;
        private readonly IEventBus _eventBus;
        public ForgotPasswordCommandHandler(IAuthIdentityService authIdentityService, IEventBus eventBus)
        {
            _authIdentityService = authIdentityService;
            _eventBus = eventBus;
        }
        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var token = await _authIdentityService.ForgotPassword(request.email);
            if(token != null)
            {
                await _eventBus.PublishAsync(new ForgotPasswordEvent(request.email, token));
            }
            return true;
        }
    }
}
