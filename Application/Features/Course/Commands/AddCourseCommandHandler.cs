using Application.Abstractions.Messaging;
using Application.Common.Interfaces;
using Application.Features.Course.Command;
using Application.Models.Course;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Course.Commands
{
    public sealed class AddCourseCommandHandler : ICommandHandler<AddCourseCommand, CourseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddCourseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException();
        }
        public async Task<CourseResponse> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Domain.Entity.Course
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Content = request.Content,
                ImageUrl = request.ImageUrl,
                IsActive = true,
                TopicId = request.TopicId,
            };
            await _unitOfWork.CourseRepository.AddAsync(course);
            return new CourseResponse(course.Id, course.Name,course.Description, course.Content, course.ImageUrl, course.TopicId);
        }
    }
}
