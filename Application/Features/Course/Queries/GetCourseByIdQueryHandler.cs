using Application.Abstractions.Messaging;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Course.Queries
{
    public sealed class GetCourseByIdQueryHandler : IQueryHandler<GetCourseByIdQuery, CourseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCourseByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException();
        }
        public async Task<CourseResponse> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Course", request.Id);
            return new CourseResponse(course.Id, course.Name, course.Description, course.Content, course.ImageUrl, course.TopicId);
        }
    }
}
