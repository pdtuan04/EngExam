using Application.Exceptions;
using Application.Handler.InterfaceHandler;
using Application.Models.Exam;
using Domain.Entity;

namespace Application.Handler
{
    public class MultipleChoiceHandler : IQuestionTypesHandler
    {
        public AnswerHistory HistoryHandler(UserAnswerRequest userAnswer, ExamDetail examDetail, Guid examResultId)
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
                Score = isCorrect ? examDetail.Score : 0,
            };
        }
        public double CalculateScoreHandler(UserAnswerRequest userAnswer, ExamDetail examDetail)
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
