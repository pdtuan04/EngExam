using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ExamResult
{
    public sealed record ExamResultDetailResponse(
        Guid Id,
        DateTime CompleteAt,
        double Score,
        double TotalScore,
        IReadOnlyCollection<UserAnswerResponse> UserAnswers)
    {
        public IReadOnlyCollection<UserAnswerResponse> UserAnswers { get; init; } = UserAnswers ?? [];
    }
}
