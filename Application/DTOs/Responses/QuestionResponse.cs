using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Exam.Doing;
using Domain.Enums;

namespace Application.DTOs.Responses
{
    public class QuestionResponse
    {
        public required Guid Id { get; set; }
        public required string Context { get; set; }
        public required QuestionTypes QuestionTypes { get; set; }
        public string Explanation { get; set; }
        public required ICollection<AnswerResponse> Answers { get; set; } = [];
    }
}
