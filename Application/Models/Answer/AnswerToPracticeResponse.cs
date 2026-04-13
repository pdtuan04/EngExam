using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Answer
{
    public sealed record AnswerToPracticeResponse(
    Guid Id,
    string Content,
    bool IsCorrect);
}
