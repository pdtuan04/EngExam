using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entity
{
    public class Question:BaseEntity
    {
        public required string Content { get; set; }
        public required QuestionTypes QuestionTypes { get; set; }
        public string? Explanation { get; set; }
        public string? ImageUrl { get; set; }
        public required Guid TopicId { get; set; }
        public ICollection<Answer> Answers { get; set; } = [];
        public ICollection<ExamDetail> ExamDetail { get; set; } = [];
        public ICollection<AnswerHistory> AnswerHistory { get; set; } = [];
    }
}
