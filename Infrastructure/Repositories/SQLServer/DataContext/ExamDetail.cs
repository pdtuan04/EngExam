using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class ExamDetail
    {
        public required Guid ExamId { get; set; }
        public required Guid QuestionId { get; set; }
        public required int Score { get; set; }
        public Exam Exam { get; set; } = null!;
        public Question Question { get; set; } = null!;
    }
}
