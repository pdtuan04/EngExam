using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ExamResult
{
    public class UserAnswerResponse
    {
        public required string Content { get; set; }
        public required QuestionTypes QuestionTypes { get; set; }
        public required string UserAnswer { get; set; }
        public string Explanation { get; set; }
        public required bool IsCorrect { get; set; }
        public required double EarnedPoint { get; set; }
        public IEnumerable<Option> Options { get; set; } = [];
    }
    public class Option
    {
        public required string Content { get; set; }
        public required bool IsCorrect { get; set; }
    }
}
