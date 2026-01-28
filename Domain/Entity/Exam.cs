using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entity
{
    public class Exam:BaseEntity
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        private int _durationInMinutes;
        public required int DurationInMinutes { 
            get
            {
                return _durationInMinutes;
            }
            set
            {
                if(value <= 0)
                    throw new ArgumentException("Duration must be greater than zero.");
                if(value > 180)
                    throw new ArgumentException("Duration must not exceed 180 minutes.");
                _durationInMinutes = value;
            }
        }
        public required Guid ExamCategoryId { get; set; }
        public ICollection<ExamDetail> ExamDetail { get; } = [];
        public void AddExamDetail(Question question, double score)
        {
            if(ExamDetail.Any(q => q.QuestionId == question.Id)) 
                throw new Exception($"Question {question.Id} already exists in the exam.");
            ExamDetail.Add(new ExamDetail() { ExamId = this.Id, QuestionId = question.Id, Score = score ,Question = question});
        }

    }
}
