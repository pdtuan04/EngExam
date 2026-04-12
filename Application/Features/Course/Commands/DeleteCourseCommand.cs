using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Course.Commands
{
    public sealed record DeleteCourseCommand(Guid Id) : ICommand<bool>;
}
