using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Answer
{
    public record AnswerReadModel(
        Guid Id,
        string Content,
        bool IsCorrect,
        Guid QuestionId,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        bool IsDeleted = false
    );
}
