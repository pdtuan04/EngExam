using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read.DataContext
{
    public class AnswerHistory
    {
        public required Guid Id { get; set; }
        public required Guid QuestionId { get; set; }
        public required string QuestionText { get; set; }
        public required QuestionTypes QuestionTypes { get; set; }
        public string? Explanation { get; set; } = null;
        public string? ImageUrl { get; set; }
        public required string OptionsJson { get; set; }
        public required string UserAnswer { get; set; }
        public required bool IsCorrect { get; set; }
        public required double Score { get; set; }
        public required Guid ExamResultId { get; set; }
    }
}
