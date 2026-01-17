using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses
{
    public class ExamSummaryDto
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
}
