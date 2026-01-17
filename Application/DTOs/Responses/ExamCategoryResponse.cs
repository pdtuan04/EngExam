using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses
{
    public class ExamCategoryResponse
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
