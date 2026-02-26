using Application.Models.Answer;
using Domain.Entity;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Question
{
    public class QuestionDetailResponse
    {
        public required Guid Id { get; set; }
        public required DateTime CreateAt { get; set; }
        public required string Content { get; set; }
        public QuestionTypes QuestionTypes { get; set; }
        public string? Explanation { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<AnswerDetailsResponse> Answers { get; set; } = [];
        public required double Score { get; init; }
        public required Guid TopicId { get; set; }
    }
}
