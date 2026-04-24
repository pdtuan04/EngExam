using System;

namespace Application.Features.Exam.Events
{
    public sealed record CreateExamEvent(
        Guid ExamId
    );
}