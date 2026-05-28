using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.Features.Comment.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Commands
{
    public sealed class DeleteCommentCommandHandler : ICommandHandler<DeleteCommentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public DeleteCommentCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }

        public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.CommentRepository.Delete(request.Id);
            var now = DateTime.UtcNow;
            await _eventBus.PublishAsync(new DeleteCommentEvent(request.Id, request.ParentId, request.CourseId, now));
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
