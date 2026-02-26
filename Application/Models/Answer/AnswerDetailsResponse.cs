using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Answer
{
    public class AnswerDetailsResponse
    {
        public required Guid Id { get; set; }
        public required string Content { get; set; }
        public required bool IsCorrect { get; set; } = false;
        public required Guid QuestionId { get; set; }
    }
}
