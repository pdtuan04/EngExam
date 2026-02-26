using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ExamResult
{
    public class ExamResultDetailResponse
    {
        public required Guid Id { get; set; }
        public required DateTime CompleteAt { get; set; }
        public required double TotalScore { get; set; }
        public ICollection<UserAnswerResponse> UserAnswers { get; set; } = [];
    }
}
