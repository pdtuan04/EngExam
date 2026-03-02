using Application.Models.Answer;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Question
{
    public class UpdateQuestionRequest
    {
        public required Guid Id { get; set; }
        public required bool IsActive { get; set; }
        [Required]
        public required string Content { get; set; }
        [Range(0.1, double.MaxValue)]
        public required double Score { get; set; }
        [Required]
        public required QuestionTypes QuestionTypes { get; set; }
        public string? Explanation { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public required Guid TopicId { get; set; }
        [MinLength(1)]
        public ICollection<UpdateAnswerRequest> Answers { get; set; } = [];
    }
}
