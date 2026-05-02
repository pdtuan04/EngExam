using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Features.Course.Command;
using Application.Features.Course.Events;
using Application.Models.Course;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Course.Commands
{
    public sealed class AddCourseCommandHandler : ICommandHandler<AddCourseCommand, CourseDetailResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public AddCourseCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException();
            _eventBus = eventBus ?? throw new ArgumentNullException();
        }
        public async Task<CourseDetailResponse> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Domain.Entity.Course
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Name = request.Name,
                Description = request.Description,
                Content = request.Content,
                ImageUrl = request.ImageUrl,
                IsActive = true,
                TopicId = request.TopicId,
            };
            await _unitOfWork.CourseRepository.AddAsync(course);
            await _eventBus.PublishAsync(new CreateCourseEvent(
                course.Id,
                course.Name,
                course.Description,
                course.Content,
                course.ImageUrl,
                course.TopicId,
                course.IsActive,
                course.CreatedAt
            ), cancellationToken);
            return new CourseDetailResponse(course.Id, course.Name,course.Description, course.Content, course.ImageUrl, course.TopicId);
        }
    }
}
