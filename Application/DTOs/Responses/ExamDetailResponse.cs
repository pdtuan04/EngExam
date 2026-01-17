using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses
{
    public class ExamDetailResponse
    {
        public required Guid Id { get; set; }
        public string Description { get; set; }

    }
}
