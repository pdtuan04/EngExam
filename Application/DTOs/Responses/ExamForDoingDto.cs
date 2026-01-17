using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses
{
    public class ExamForDoingDto
    {
        public required Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public required ICollection<QuestionResponse> Questions { get; set; } = [];
    }
}
