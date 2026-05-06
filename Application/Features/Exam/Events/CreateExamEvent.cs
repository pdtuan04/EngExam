using System;

namespace Application.Features.Exam.Events
{
    public sealed record CreateExamEvent(
        Guid ExamId,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        string Title,
        string Description,
        int DurationInMinutes,
        Guid ExamCategoryId
    );
}