using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Question
{
    public record CreateQuestionGroupRequest(
        string GroupContent,
        string? Title = null,
        string? ImageUrl = null,
        string? AudioUrl = null,
        IReadOnlyCollection<CreateQuestionRequest>? Questions = null)
    {
        public IReadOnlyCollection<CreateQuestionRequest> Questions { get; init; } = Questions ?? [];
    }
}
