using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Requests.Question;
using Application.Exceptions;
using Application.Handler.InterfaceHandler;
using Domain.Entity;

namespace Application.Handler
{
    public class MultipleChoiceHandler : IQuestionTypesHandler
    {
        public AnswerHistory HistoryHandler(UserAnswer userAnswer, ExamDetail examDetail, Guid examResultId)
        {
            var answer = examDetail
                         .Question.Answers
                         .FirstOrDefault(a => a.Id == userAnswer.AnswerId) ?? throw new AnswerNullException();
            bool isCorrect = answer?.IsCorrect ?? false;
            return new AnswerHistory
            {
                ExamResultId = examResultId,
                QuestionId = examDetail.QuestionId,
                UserAnswer = answer?.Content ?? "",
                IsCorrect = isCorrect,
                Score = isCorrect ? examDetail.Score : 0
            };
        }
        public double CalculateScoreHandler(UserAnswer userAnswer, ExamDetail examDetail)
        {
            if(!userAnswer.AnswerId.HasValue) return 0;
            var correctAnswer = examDetail
                                .Question
                                .Answers
                                .FirstOrDefault(a => a.IsCorrect) ?? throw new Exception("This question doesn't has any correct answer");
            if(userAnswer.AnswerId == correctAnswer.Id)
            {
                return examDetail.Score;
            }
            return 0;
        }
    }
}
