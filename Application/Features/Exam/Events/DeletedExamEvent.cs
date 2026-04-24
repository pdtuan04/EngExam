using System;

namespace Application.Features.Exam.Events
{
    public sealed record DeletedExamEvent(Guid Id);
}