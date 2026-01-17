using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses
{
    public class ExamTakeResponse
    {
        public required Guid Id { get; set; }
        public string Description { get; set; }
        public required ICollection<QuestionForExam> Questions { get; set; } = [];
    }
}
