using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Exam;
using Domain.Entity;

namespace Application.Handler.InterfaceHandler
{
    public interface IQuestionTypesHandler
    {
        AnswerHistory HistoryHandler(UserAnswerRequest userAnswer, ExamDetail examDetail, Guid examResultId);
        double CalculateScoreHandler(UserAnswerRequest userAnswer, ExamDetail examDetail);
    }
}
