using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.Comment.Events;
using Application.Models.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Commands
{
    public sealed class AddCommentCommandHandler : ICommandHandler<AddCommentCommand, CommentResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        private readonly IAuthIdentityService _authIdentityService;

        public AddCommentCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus, IAuthIdentityService authIdentityService)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
            _authIdentityService = authIdentityService;
        }
        public async Task<CommentResponse> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var user = await _authIdentityService.GetByIdAsync(request.userId) ?? throw new NotFoundException("User not found", request.userId);
            Domain.Entity.Comment comment;
            if (request.parentId != null)
            {
                var parentComment = await _unitOfWork.CommentRepository.GetByIdAsync(request.parentId);
                if (parentComment == null) throw new NotFoundException("Parent Comment not found", request.parentId);
                comment = parentComment.Reply(request.userId, request.content);
            }
            else
            {
                comment = Domain.Entity.Comment.CreateRoot(request.courseId, request.userId, request.content);
            }
            await _unitOfWork.CommentRepository.AddAsync(comment);
            var commentAddedEvent = new CreateCommentEvent(comment.Id, comment.CourseId, comment.UserId, user.UserName, user.AvatarUrl, comment.Content, comment.ParentId, comment.RootCommentId, comment.Path, comment.Level, comment.IsDeleted, comment.CreatedAt);
            await _eventBus.PublishAsync(commentAddedEvent);
            await _unitOfWork.SaveChangesAsync();
            return new CommentResponse(comment.Id, comment.CourseId, comment.UserId, user.UserName, user.AvatarUrl, comment.Content, comment.ParentId, comment.RootCommentId, comment.Path, comment.Level, comment.IsDeleted, 0);
        }
    }
}
