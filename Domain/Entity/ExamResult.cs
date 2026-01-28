using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class ExamResult
    {
        public required Guid Id { get; set; }
        public required DateTime CompleteAt { get; set; }
        public required Guid ExamId { get; set; }
        public Exam? Exam { get; set; }
        private double _score;
        public required double Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
            }
        }
        public required ICollection<AnswerHistory> AnswerHistory { get; set; }
        public required Guid UserId { get; set; }
    }
}
