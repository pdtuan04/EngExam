using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.Common.Constants;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Exceptions;
using Application.Features.User.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands
{
    public sealed class SignUpCommandHandler : ICommandHandler<SignUpCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthIdentityService _authIdentityService;
        private readonly IEventBus _eventBus;
        public SignUpCommandHandler(IUnitOfWork unitOfWork, IAuthIdentityService authIdentityService, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _authIdentityService = authIdentityService;
            _eventBus = eventBus;
        }
        public async Task<bool> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            if (!string.Equals(request.Password, request.ConfirmPassword)) throw new BadRequestException("Password and ConfirmPassword not match");
            if (await _authIdentityService.CheckUserExist(request.UserName, request.Password) == true) throw new AccountRegisterFailedException();

            var user = new Domain.Entity.User
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Password = request.Password,
                Age = request.Age,
                Email = request.Email,
                CreatedAt = now,
                UpdatedAt = now
            };
            var resutl = await _authIdentityService.SignUp(user);
            if (!resutl) throw new AccountRegisterFailedException("Sign up unsuccess");
            await _authIdentityService.CreateRole(Roles.User);
            await _authIdentityService.AddUserToRole(user, Roles.User);
            await _eventBus.PublishAsync(new UserCreatedEvent(user.Id, user.UserName, user.Email, user.Age, new[] { Roles.User }, user.CreatedAt, user.UpdatedAt));
            return resutl;
        }
    }
}
