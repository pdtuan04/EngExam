using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Answer
{
    public class CreateAnswerRequest
    {
        [Required]
        public required string Content { get; set; }
        public required bool IsCorrect { get; set; }
    }
}
