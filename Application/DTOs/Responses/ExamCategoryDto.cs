using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses
{
    public class ExamCategoryDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
