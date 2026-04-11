using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Course
{
    public sealed record CreateCourseRequest(
        string Name,
        string Description,
        string Content,
        string? ImageUrl,
        Guid TopicId
    );
}
