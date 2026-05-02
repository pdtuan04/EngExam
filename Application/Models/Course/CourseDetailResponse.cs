using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Course
{
    public sealed record CourseDetailResponse(
        Guid Id,
        string Name,
        string Description,
        string Content,
        string? ImageUrl,
        Guid TopicId
    );
}
