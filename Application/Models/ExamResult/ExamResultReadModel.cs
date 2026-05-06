using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ExamResult
{
    public record ExamResultReadModel(
        Guid Id,
        string Title,
        string? Description,
        int DurationInMinutes,
        DateTime CompleteAt,
        double Score,
        Guid ExamId,
        Guid UserId
    );

    public record AnswerHistoryReadModel(
        Guid Id,
        Guid QuestionId,
        string QuestionText,
        QuestionTypes QuestionTypes,
        string? Explanation,
        string? ImageUrl,
        string OptionsJson,
        string UserAnswer,
        bool IsCorrect,
        double Score,
        Guid ExamResultId
    );
}
