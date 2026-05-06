using Application.Models.Answer;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Question
{
    public record QuestionReadModel(
        Guid Id,
        string Content,
        QuestionTypes QuestionTypes, 
        string? Explanation,
        string? ImageUrl,
        Guid TopicId,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        bool IsDeleted = false,
        bool IsActive = true
    );
}
