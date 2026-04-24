using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Practice.Commands
{
    public sealed record DeletePracticeCommand(Guid Id) : ICommand<bool>;
}
