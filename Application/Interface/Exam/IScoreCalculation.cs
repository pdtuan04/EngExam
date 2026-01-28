using Application.DTOs.Requests.Question;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Exam
{
    public interface IScoreCalculation
    {
        double ScoreCalculation(ICollection<UserAnswer> userAnswers, ICollection<ExamDetail> examDetails);
    }
}
