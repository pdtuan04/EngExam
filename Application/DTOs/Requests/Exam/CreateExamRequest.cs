using Application.DTOs.Requests.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests.Exam
{
    public class CreateExamRequest
    {
        public required string Description { get; set; }
        public required Guid ExamCategoryId { get; set; }
        public required ICollection<CreateQuestionRequest> Questions { get; set; } = [];
    }
}
