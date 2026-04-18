using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Common.Exceptions;
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
        private readonly ICourseReadRepository _courseReadRepository;
        public GetCourseByIdQueryHandler(ICourseReadRepository courseReadRepositor)
        {
            _courseReadRepository = courseReadRepositor;
        }
        public async Task<CourseResponse> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseReadRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Course", request.Id);
            return new CourseResponse(course.Id, course.Name, course.Description, course.Content, course.ImageUrl, course.TopicId);
        }
    }
}
