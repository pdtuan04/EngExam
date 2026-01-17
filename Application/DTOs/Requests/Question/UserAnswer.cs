using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests.Question
{
    public class UserAnswer
    {
        public Guid QuestionId { get; set; }
        public Guid? AnswerId { get; set; }
        public string? AnswerFillInBlank { get; set; }
    }
}
