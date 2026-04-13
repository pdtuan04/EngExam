using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ExamResult
{
    public sealed record UserAnswerResponse(
        string Content,
        QuestionTypes QuestionTypes,
        string UserAnswer,
        bool IsCorrect,
        double EarnedPoint,
        string? Explanation = null,
        IReadOnlyCollection<Option> Options = null!)
    {
        public IReadOnlyCollection<Option> Options { get; init; } = Options ?? [];
    }
    public sealed record Option(string Content, bool IsCorrect);
}
