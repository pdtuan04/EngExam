using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Enums;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class Question:BaseEntity
    {
        public required string Content { get; set; }
        public QuestionTypes QuestionTypes { get; set; }
        public string? Explanation { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<Answer> Answers { get; set; } = null!;
        public ICollection<ExamDetail> ExamDetail { get; set; } = null!;
        public ICollection<AnswersHistory> AnswerHistory { get; set; } = null!;
        public ICollection<PracticeDetail> PracticeDetails { get; set; } = null!;
        public required Guid TopicId { get; set; }
        public Topic? Topic { get; set; } = null!;

    }
}
