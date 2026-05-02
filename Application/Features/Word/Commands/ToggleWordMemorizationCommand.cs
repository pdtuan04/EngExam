using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Word.Commands
{
    public sealed record ToggleWordMemorizationCommand(Guid Id) : ICommand<bool>;
}
