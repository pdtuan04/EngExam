using System;

namespace Application.Features.Exam.Events
{
    public sealed record UpdateExamEvent(
        Guid ExamId,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        string Title,
        string Description,
        int DurationInMinutes,
        Guid ExamCategoryId
    );
}