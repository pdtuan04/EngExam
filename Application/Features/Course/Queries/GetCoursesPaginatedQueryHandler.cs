using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
using Application.Models.Course;
using Application.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Course.Queries
{
    public sealed class GetCoursesPaginatedQueryHandler : IQueryHandler<GetCoursesPaginatedQuery, PaginationResponse<CourseResponse>>
    {
        private readonly ICourseReadRepository _courseReadRepository;
        public GetCoursesPaginatedQueryHandler(ICourseReadRepository courseReadRepository)
        {
            _courseReadRepository = courseReadRepository;
        }
        public async Task<PaginationResponse<CourseResponse>> Handle(GetCoursesPaginatedQuery request, CancellationToken cancellationToken)
        {
            return await _courseReadRepository.ToPagination<CourseResponse>(request.pageIndex, request.pageSize, cancellationToken:cancellationToken);
        }
    }
}
