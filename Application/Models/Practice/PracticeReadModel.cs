using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Practice
{
    public record PracticeReadModel(
        Guid Id,
        string Title,
        string? Description,
        Guid TopicId,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        bool IsDeleted = false
    );
    public record PracticeDetailReadModel(Guid PracticeId, Guid QuestionId);
}
