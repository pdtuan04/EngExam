using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Requests.Question;
using Application.Handler.InterfaceHandler;
using Domain.Entity;

namespace Application.Handler
{
    public class FillInBlankHandler:IQuestionTypesHandler
    {
        public AnswerHistory HistoryHandler(UserAnswer userAnswer, ExamDetail examDetail, Guid examResultId)
        {
            var isCorrectFill = examDetail
                                .Question.Answers
                                .Any(a => string.Equals(a.Content, userAnswer.AnswerFillInBlank, StringComparison.OrdinalIgnoreCase));
            if (isCorrectFill)
            {
                return new AnswerHistory
                {
                    ExamResultId = examResultId,
                    QuestionId = examDetail.QuestionId,
                    UserAnswer = userAnswer?.AnswerFillInBlank ?? "",
                    IsCorrect = true,
                    Score = isCorrectFill ? examDetail.Score : 0
                };
            }
            return new AnswerHistory
            {
                ExamResultId = examResultId,
                QuestionId = examDetail.QuestionId,
                UserAnswer = userAnswer?.AnswerFillInBlank ?? "",
                IsCorrect = false,
                Score = 0
            };
        }
        public double CalculateScoreHandler(UserAnswer userAnswer, ExamDetail examDetail)
        {
            if(string.IsNullOrWhiteSpace(userAnswer.AnswerFillInBlank)) return 0;
            var correctAnswer = examDetail.Question.Answers
                                .FirstOrDefault(a => a.IsCorrect == true) ?? throw new Exception("This question has no correct answer");
            if (userAnswer.AnswerFillInBlank == correctAnswer.Content) return examDetail.Score;
            return 0;
        }
    }
}
