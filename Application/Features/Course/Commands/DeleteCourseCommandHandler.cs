using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Features.Course.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Course.Commands
{
    public class DeleteCourseCommandHandler : ICommandHandler<DeleteCourseCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public DeleteCourseCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException();
            _eventBus = eventBus ?? throw new ArgumentNullException();
        }
        public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.CourseRepository.Delete(request.Id);
            await _eventBus.PublishAsync(new DeletedCourseEvent(request.Id), cancellationToken);
            return result;  
        }
    }
}
