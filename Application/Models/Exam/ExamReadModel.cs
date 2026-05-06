using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Exam
{
    public record ExamReadModel(
        Guid Id,
        string Title,
        string? Description,
        int DurationInMinutes,
        Guid ExamCategoryId,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        bool IsDeleted = false
    );
    public record ExamDetailReadModel(
        Guid ExamId,
        Guid QuestionId,
        double Score
    );
}
