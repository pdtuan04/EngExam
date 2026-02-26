using Application.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Practice
{
    public class DoPracticeResponse
    {
        public required Guid Id { get; set; }
        public string? Description { get; set; }
        public ICollection<QuestionToPracticeResponse> Questions { get; set; } = [];
    }
}
