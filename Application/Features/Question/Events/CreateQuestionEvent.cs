using Application.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Question.Events
{
    public sealed record CreateQuestionEvent(IReadOnlyCollection<QuestionReadModel> Questions);
}
