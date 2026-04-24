using System;

namespace Application.Features.Exam.Events
{
    public sealed record UpdateExamEvent(
        Guid ExamId,
        Guid ExamCategoryId
    );
}