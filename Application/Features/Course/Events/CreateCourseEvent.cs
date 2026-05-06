using System;

namespace Application.Features.Course.Events
{
    public sealed record CreateCourseEvent(
        Guid CourseId,
        string Name,
        string Description,
        string Content,
        string? ImageUrl,
        Guid TopicId,
        bool IsActive,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}