using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Commands
{
    public sealed record DeleteCommentCommand(Guid Id, Guid? ParentId, Guid CourseId) : ICommand<bool>;
}
