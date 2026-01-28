using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Exam.Practice
{
    public class ExamForPracticeDTO
    {
        public required Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public required ICollection<QuestionForPracticeDTO> Questions { get; set; } = [];
    }
}
