using Application.Models.ExamResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamResult.Events
{
    public record CreateExamResultEvent(ExamResultReadModel ExamResult);
}
