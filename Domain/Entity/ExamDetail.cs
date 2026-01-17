using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class ExamDetail
    {
        public required Guid ExamId { get; set; }
        public required Guid QuestionId { get; set; }
        private double _score;
        public required double Score 
        {
            get
            {
                return _score; 
            }
            set
            {
                if (value < 0) throw new ArgumentException("Score can't be negative");
                else _score = value;
            }
        }
        public Exam Exam { get; set; }
        public Question Question { get; set; }
    }
}
