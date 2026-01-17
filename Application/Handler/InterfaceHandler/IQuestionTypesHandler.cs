using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Requests.Question;
using Domain.Entity;

namespace Application.Handler.InterfaceHandler
{
    public interface IQuestionTypesHandler
    {
        AnswerHistory HistoryHandler(UserAnswer userAnswer, ExamDetail examDetail, Guid examResultId);
        double CalculateScoreHandler(UserAnswer userAnswer, ExamDetail examDetail);
    }
}
