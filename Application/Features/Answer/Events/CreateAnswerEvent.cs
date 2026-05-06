using Application.Models.Answer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Answer.Events
{
    public sealed record CreateAnswerEvent(IReadOnlyCollection<AnswerReadModel> Answers);
}
