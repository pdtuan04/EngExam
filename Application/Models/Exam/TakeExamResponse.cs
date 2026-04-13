using Application.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Exam
{
    public sealed record TakeExamResponse(
        Guid Id,
        string? Title,
        string? Description,
        int DurationInMinutes,
        IReadOnlyCollection<QuestionToTakeResponse> Questions)
    {
        public IReadOnlyCollection<QuestionToTakeResponse> Questions { get; init; } = Questions ?? [];
    }
}
