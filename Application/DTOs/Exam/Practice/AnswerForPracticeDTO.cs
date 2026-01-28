using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Exam.Practice
{
    public class AnswerForPracticeDTO
    {
        public required Guid Id { get; set; }
        public required string Content { get; set; }
    }
}
