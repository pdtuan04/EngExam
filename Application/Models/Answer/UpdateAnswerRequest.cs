using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Answer
{
    public record UpdateAnswerRequest(
    Guid Id,
    bool IsActive,
    string Content,
    bool IsCorrect);
}
