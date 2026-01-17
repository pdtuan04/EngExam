using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class ExamDetail
    {
        public Guid ExamId { get; set; }
        public Guid QuestionId { get; set; }
        public int Score { get; set; }
        public Exam Exam { get; set; }
        public Question Question { get; set; }
    }
}
