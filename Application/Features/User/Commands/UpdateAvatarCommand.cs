using Application.Abstractions.Messaging;
using Application.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Features.User.Commands
{
    public sealed record UpdateAvatarCommand(Guid UserId, string AvatarUrl) : ICommand<UserDetailResponse>;
}
