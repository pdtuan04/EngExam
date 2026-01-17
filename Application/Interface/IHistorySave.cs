using Application.DTOs.Requests.Question;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IHistorySave
    {
        ICollection<AnswerHistory> HistorySave(ICollection<UserAnswer> userAnswers, ICollection<ExamDetail> examDetails, Guid examResultId);
    }
}
