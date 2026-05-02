using Application.Abstractions.Messaging;
using Application.Models.Course;
using Application.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Course.Queries
{
    public sealed record GetCoursesPaginatedQuery(int pageIndex = 1, int pageSize = 10) : IQuery<PaginationResponse<CourseResponse>>;
}
