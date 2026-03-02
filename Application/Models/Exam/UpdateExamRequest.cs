using Application.Models.Question;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Exam
{
    public class UpdateExamRequest
    {
        public required Guid Id { get; set; }
        public required bool IsActive { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required int DurationInMinutes { get; set; }
        public string? ImageUrl { get; set; }
        public required ICollection<UpdateQuestionRequest> Questions { get; set; } = [];
        public required Guid ExamCategoryId { get; set; }
    }
}
