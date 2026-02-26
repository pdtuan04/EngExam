using Application.Models.Answer;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Question
{
    public class QuestionToPracticeResponse
    {
        public required Guid Id { get; set; }
        public required string Content { get; set; }
        public required QuestionTypes QuestionTypes { get; set; }
        public string? Explanation { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<AnswerToPracticeResponse> Answers { get; set; } = [];
    }
}
