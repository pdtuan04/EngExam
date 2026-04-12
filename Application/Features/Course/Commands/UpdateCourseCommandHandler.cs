using Application.Abstractions.Messaging;
using Application.Common.Interfaces;
using Application.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Course.Commands
{
    public sealed class UpdateCourseCommandHandler : ICommandHandler<UpdateCourseCommand, CourseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        UpdateCourseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException();
        }
        public async Task<CourseResponse> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(request.Id);
            course.Name = request.Name;
            course.Description = request.Description;
            course.Content = request.Content;
            course.ImageUrl = request.ImageUrl;
            course.TopicId = request.TopicId;
            await _unitOfWork.CourseRepository.Update(course);
            return new CourseResponse(course.Id, course.Name, course.Description, course.Content, course.ImageUrl, course.TopicId);
        }
    }
}
