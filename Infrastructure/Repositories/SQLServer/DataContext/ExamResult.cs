using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class ExamResult
    {
        public required Guid Id { get; set; }
        public required DateTime CompleteAt { get; set; }
        public required Guid ExamId { get; set; }
        public Exam Exam { get; set; } = null!;
        public double Score { get; set; } = 0;
        public required ICollection<AnswersHistory> AnswerHistory { get; set; }
        public required Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
