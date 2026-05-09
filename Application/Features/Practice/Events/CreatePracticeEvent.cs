using Application.Models.Answer;
using Application.Models.Exam;
using Application.Models.Practice;
using Application.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Practice.Events
{
    public sealed record CreatePracticeEvent(PracticeReadModel Practice, 
        IReadOnlyCollection<QuestionReadModel> Questions,
        IReadOnlyCollection<AnswerReadModel> Answers,
        IReadOnlyCollection<PracticeDetailReadModel> Details);
}
