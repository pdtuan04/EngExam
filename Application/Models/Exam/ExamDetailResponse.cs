using Application.Models.Question;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Exam
{
    public record ExamDetailResponse(
        Guid Id,
        DateTime CreatedAt,
        string Title,
        int DurationInMinutes,
        Guid ExamCategoryId,
        string? Description = null,
        IReadOnlyCollection<QuestionDetailResponse> Questions = null!)
    {
        public IReadOnlyCollection<QuestionDetailResponse> Questions { get; init; } = Questions ?? [];
    }
}
