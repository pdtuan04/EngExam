using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
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
        public AddCommentCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }
        public async Task<CommentResponse> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var commentDetail = await _unitOfWork.GetRepository<Domain.Entity.Comment>().GetAsync(c => c.Id == request.parentId);
            var comment = new Domain.Entity.Comment
            {
                Id = request.parentId == Guid.Empty ? Guid.NewGuid() : request.parentId,
                CourseId = request.courseId,
                UserId = request.userId,
                Content = request.content,
                ParentId = request.parentId == Guid.Empty ? null : request.parentId,
                RootCommentId = request.parentId == Guid.Empty ? Guid.NewGuid() : request.parentId,
                Path = string.Empty,
                Level = 0,
                IsDeleted = false
            };
        }
    }
}
