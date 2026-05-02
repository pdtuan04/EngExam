using Application.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Practice
{
    public sealed record UpdatePracticeRequest(
        bool IsActive,
        string Title,
        Guid TopicId,
        string? Description = null,
        IReadOnlyCollection<UpdateQuestionRequest> Questions = null!)
    {
        public IReadOnlyCollection<UpdateQuestionRequest> Questions { get; init; } = Questions ?? [];
    }
}
