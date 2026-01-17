using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Requests.Answer;
using Domain.Enums;

namespace Application.DTOs.Requests.Question
{
    public class CreateQuestionRequest
    {
        public required string Context { get; set; }
        public required QuestionTypes QuestionTypes { get; set; }
        public string Explanation { get; set; }
        public Guid TopicId { get; set; }
        public ICollection<CreateAnswerRequest> Answers { get; set; } = [];
    }
}
