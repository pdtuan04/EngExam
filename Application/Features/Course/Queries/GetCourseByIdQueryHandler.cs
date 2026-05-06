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
    public sealed class GetCourseByIdQueryHandler : IQueryHandler<GetCourseByIdQuery, CourseDetailResponse>
    {
        private readonly ICourseReadRepository _courseReadRepository;
        public GetCourseByIdQueryHandler(ICourseReadRepository courseReadRepositor)
        {
            _courseReadRepository = courseReadRepositor;
        }
        public async Task<CourseDetailResponse> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            return await _courseReadRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException("Course", request.Id);
        }
    }
}
