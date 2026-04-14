using Application.Abstractions.Messaging;
using Application.Models.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User.Commands
{
    public sealed record SignInCommand(string username, string password, bool rememberme) : ICommand<SignInResponse>;
}
