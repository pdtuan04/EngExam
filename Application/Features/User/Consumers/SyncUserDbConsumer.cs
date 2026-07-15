using Application.Abstractions.Repositories.Read;
using Application.Features.User.Commands;
using Application.Features.User.Events;
using Application.Models.User;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Consumers
{
    public sealed class SyncUserDbConsumer : IConsumer<UserAvatarUpdatedEvent>, IConsumer<UserCreatedEvent>
    {
        private readonly IUserReadRepository _userReadRepository;
        public SyncUserDbConsumer(IUserReadRepository userReadRepository)
        {
            _userReadRepository = userReadRepository;
        }
        public async Task Consume(ConsumeContext<UserAvatarUpdatedEvent> context)
        {
            await _userReadRepository.UpdateUserAvatarAsync(context.Message.UserId, context.Message.AvatarUrl, context.Message.UpdatedAt);
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            await _userReadRepository.UpsertAsync(new UserReadModel(
                context.Message.Id,
                context.Message.UserName,
                context.Message.Email,
                context.Message.Age,
                null,
                context.Message.CreatedAt,
                context.Message.UpdatedAt
            ));
        }
    }
}
