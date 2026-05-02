using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Features.Course.Events;
using Application.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Course.Commands
{
    public sealed class UpdateCourseCommandHandler : ICommandHandler<UpdateCourseCommand, CourseDetailResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public UpdateCourseCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException();
            _eventBus = eventBus ?? throw new ArgumentNullException();
        }
        public async Task<CourseDetailResponse> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(request.Id);
            course.Name = request.Name;
            course.Description = request.Description;
            course.Content = request.Content;
            course.ImageUrl = request.ImageUrl;
            course.TopicId = request.TopicId;
            await _unitOfWork.CourseRepository.Update(course);
            await _eventBus.PublishAsync(new UpdateCourseEvent(
                                            course.Id, course.Name, 
                                            course.Description, course.Content, 
                                            course.ImageUrl, 
                                            course.TopicId, 
                                            course.IsActive, 
                                            course.CreatedAt), cancellationToken);
            return new CourseDetailResponse(course.Id, course.Name, course.Description, course.Content, course.ImageUrl, course.TopicId);
        }
    }
}
