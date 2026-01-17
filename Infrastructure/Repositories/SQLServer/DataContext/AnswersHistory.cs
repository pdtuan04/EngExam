using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class AnswersHistory
    {
        public required Guid ExamResultId { get; set; }
        public required Guid QuestionId { get; set; }
        public required ExamResult ExamResult { get; set; }
        public required Question Question { get; set; }
        public required string UserAnswer { get; set; }
        public required bool IsCorrect { get; set; }
        public required double Score { get; set; }
    }
}
