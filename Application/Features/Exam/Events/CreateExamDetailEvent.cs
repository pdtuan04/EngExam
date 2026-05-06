using Application.Models.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exam.Events
{
    public sealed record CreateExamDetailEvent(IReadOnlyCollection<ExamDetailReadModel> ExamDetails);
}
