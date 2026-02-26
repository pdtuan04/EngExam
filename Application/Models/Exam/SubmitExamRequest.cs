using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Exam
{
    public class SubmitExamRequest
    {
        [Required]
        public Guid ExamId { get; set; }
        public ICollection<UserAnswerRequest> UserAnswers { get; set; } = [];
    }
    public class UserAnswerRequest
    {
        public Guid QuestionId { get; set; }
        public Guid? AnswerId { get; set; }
        public string? AnswerFillInBlank { get; set; }
    }
}
