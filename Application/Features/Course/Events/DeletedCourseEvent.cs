using System;

namespace Application.Features.Course.Events
{
    public sealed record DeletedCourseEvent(Guid Id);
}