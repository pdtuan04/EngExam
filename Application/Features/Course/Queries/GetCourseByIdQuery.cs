using Application.Abstractions.Messaging;
using Application.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Course.Queries
{
    public sealed record GetCourseByIdQuery(Guid Id) : ICacheQuery<CourseResponse>
    {
        public string CacheKey => $"course_{Id}";

        public TimeSpan? Expiration => null;
    }
}
