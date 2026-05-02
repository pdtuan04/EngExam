using Application.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Practice
{
    public record CreatePracticeRequest(
        string Title,
        Guid TopicId,
        string? Description = null,
        IReadOnlyCollection<CreateQuestionRequest> Questions = null!)
    {
        public IReadOnlyCollection<CreateQuestionRequest> Questions { get; init; } = Questions ?? [];
    }
}
