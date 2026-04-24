using Application.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Practice
{
    public sealed record PracticeDetailResponse(
    Guid Id,
    string Title,
    Guid TopicId,
    DateTime? CreatedAt,
    string? Description = null,
    IReadOnlyCollection<QuestionToPracticeResponse> Questions = null!)
    {
        public IReadOnlyCollection<QuestionToPracticeResponse> Questions { get; init; } = Questions ?? [];
    }
}
