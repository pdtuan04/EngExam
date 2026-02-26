using Application.Models.Answer;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Question
{
    public class QuestionToTakeResponse
    {
        public required Guid Id { get; set; }
        public required string Content { get; set; }
        public required QuestionTypes QuestionTypes { get; set; }
        public required ICollection<AnswerToTakeResponse> Answers { get; set; } = [];
    }
}
