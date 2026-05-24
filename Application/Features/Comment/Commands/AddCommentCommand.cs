using Application.Abstractions.Messaging;
using Application.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Commands
{
    public sealed record AddCommentCommand(Guid parentId, string content, Guid courseId, Guid userId) : ICommand<CommentResponse>;
}
