using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Features.User.Commands
{
    public sealed record ResetPasswordCommand(string Email, string Token, string NewPassword) : ICommand<bool>;
}
