using Application.Common.Exceptions;
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
            if (userAnswer == null)
            return new AnswerHistory
            {
                Id = Guid.NewGuid(),
                ExamResultId = examResultId,
                QuestionId = examDetail.QuestionId,
                UserAnswer = "",
                IsCorrect = false,
                Score = 0,
            };
            var answer = examDetail
                         .Question.Answers
                         .FirstOrDefault(a => a.Id == userAnswer.AnswerId) ?? throw new BadRequestException("Answer isn't in the exam");
            bool isCorrect = answer?.IsCorrect ?? false;
            return new AnswerHistory
            {
                Id = Guid.NewGuid(),
                ExamResultId = examResultId,
                QuestionId = examDetail.QuestionId,
                UserAnswer = answer?.Content ?? "",
                IsCorrect = isCorrect,
                Score = isCorrect ? examDetail.Score : 0,
            };
        }
        public double CalculateScoreHandler(UserAnswerRequest userAnswer, ExamDetail examDetail)
        {
            if (userAnswer == null) return 0;
            if(!userAnswer.AnswerId.HasValue) return 0;
            var correctAnswer = examDetail
                                .Question
                                .Answers
                                .FirstOrDefault(a => a.IsCorrect) ?? throw new BadRequestException("The exam doesn't have any correct answer");
            if(userAnswer.AnswerId == correctAnswer.Id)
            {
                return examDetail.Score;
            }
            return 0;
        }
    }
}
