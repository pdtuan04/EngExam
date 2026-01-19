using Domain.Entity;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Exam.Management
{
    public class CreateExamDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public required string Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public required Guid ExamCategoryId { get; set; }
        [MinLength(1)]
        public required ICollection<QuestionInCreateExamDTO> QuestionInCreateExam { get; set; } = [];

    }
    public class QuestionInCreateExamDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public required string Content { get; set; }
        [Range(0.1, double.MaxValue)]
        public required double Score { get; set; }
        [Required]
        public required QuestionTypes QuestionTypes { get; set; }
        public string? Explanation { get; set; }
        [Required]
        public required Guid TopicId { get; set; }
        [MinLength(1)]
        public ICollection<AnswerInCreateExamDTO> Answers { get; set; } = [];
    }
    public class AnswerInCreateExamDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public required string Content { get; set; }
        public required bool IsCorrect { get; set; }
    }

}
