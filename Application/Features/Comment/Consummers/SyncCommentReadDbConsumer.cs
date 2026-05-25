using Application.Abstractions.Repositories.Read;
using Application.Features.Comment.Events;
using Application.Models.Comment;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Consummers
{
    public sealed class SyncCommentReadDbConsumer : IConsumer<CreateCommentEvent>, IConsumer<DeleteCommentEvent>
    {
        private readonly ICommentReadRepository _commentReadRepository;
        public SyncCommentReadDbConsumer(ICommentReadRepository commentReadRepository)
        {
            _commentReadRepository = commentReadRepository;
        }
        public async Task Consume(ConsumeContext<CreateCommentEvent> context)
        {
            var message = context.Message;
            await _commentReadRepository.UpsertAsync(new CommentReadModel(
                message.Id, 
                message.CourseId, 
                message.UserId, 
                message.UserName, 
                message.UserAvatarUrl,
                message.Content,
                message.RootCommentId,
                message.Path, 
                message.Level, 
                message.CreatedAt,
                message.CreatedAt, 
                message.IsDeleted,
                message.ParentId));
        }

        public async Task Consume(ConsumeContext<DeleteCommentEvent> context)
        {
            var message = context.Message;
            await _commentReadRepository.DeleteAsync(message.Id, message.DeletedAt);
        }
    }
}
