using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Practice : BaseEntity, ISoftDeletable
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public ICollection<PracticeDetail> PracticeDetails { get; set; } = [];
        public bool IsDeleted { get; set; }
        public required Guid TopicId { get; set; }
        public void AddPracticeDetail(Question question)
        {
            if (PracticeDetails.Any(q => q.QuestionId == question.Id))
                throw new Exception($"Question {question.Id} already exists in the practice.");
            PracticeDetails.Add(new PracticeDetail() { PracticeId = this.Id, QuestionId = question.Id,Question = question, });
        }
        public void UpdatePracticeDetail(Question question)
        {
            var practiceDetail = PracticeDetails.FirstOrDefault(q => q.QuestionId == question.Id);
            if (practiceDetail == null)
                PracticeDetails.Add(new PracticeDetail() { PracticeId = this.Id, QuestionId = question.Id, Question = question });
            else
            {
                practiceDetail.Question.Content = question.Content;
                practiceDetail.Question.UpdatedAt = question.UpdatedAt;
                practiceDetail.Question.Explanation = question.Explanation;
                practiceDetail.Question.ImageUrl = question.ImageUrl;
                practiceDetail.Question = question;
            }
        }
    }
}
