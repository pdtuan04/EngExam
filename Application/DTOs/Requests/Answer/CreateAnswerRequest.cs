using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Requests.Answer
{
    public class CreateAnswerRequest
    {
        public required string Context { get; set; }
        public required bool IsCorrect { get; set; } = false;
    }
}
