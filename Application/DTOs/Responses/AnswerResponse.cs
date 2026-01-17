using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses
{
    public class AnswerResponse
    {
        public required Guid Id { get; set; }
        public required string Context { get; set; }
        //public required bool IsCorrect { get; set; }
    }
}
