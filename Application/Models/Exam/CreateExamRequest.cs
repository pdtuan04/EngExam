using Application.Models.Question;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Exam
{
    public class CreateExamRequest
    {
        [Required]
        public required string Title { get; set; }
        [Required]
        [Range(1, 180)]
        public required int DurationInMinutes { get; set; }
        public string? Description { get; set; }
        [Required]
        public required Guid ExamCategoryId { get; set; }
        [MinLength(1)]
        public required ICollection<CreateQuestionRequest> Questions { get; set; } = [];

    }
}
