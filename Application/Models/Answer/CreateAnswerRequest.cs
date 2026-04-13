using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Answer
{
    public record CreateAnswerRequest(string Content, bool IsCorrect);
}
