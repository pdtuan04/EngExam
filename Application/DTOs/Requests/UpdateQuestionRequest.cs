using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.DTOs.Requests
{
    public class UpdateQuestionRequest
    {
        public required Guid Id { get; set; }
        public required string Context { get; set; }
        public required QuestionTypes QuestionTypes { get; set; }
        public required Guid TopicId { get; set; }
        public string Explanation { get; set; }
    }
}
