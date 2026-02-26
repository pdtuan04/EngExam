using Application.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Exam
{
    public class TakeExamResponse
    {
        public required Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public required int DurationInMinutes { get; set; }
        public required ICollection<QuestionToTakeResponse> Questions { get; set; } = [];
    }
}
