using Application.DTOs.Responses;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Exam.Doing
{
    public class QuestionForDoingDTO
    {
        public required Guid Id { get; set; }
        public required string Context { get; set; }
        public required QuestionTypes QuestionTypes { get; set; }
        public required ICollection<AnswerForDoingDTO> Answers { get; set; } = [];
    }
}
