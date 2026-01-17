using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses
{
    public class ExamResultDto
    {
        public required Guid Id { get; set; }
        public required DateTime CompleteAt{ get; set; }
        public required double TotalScore { get; set; }
        public required IEnumerable<UserAnswerDto> Details { get; set; }

    }
}
