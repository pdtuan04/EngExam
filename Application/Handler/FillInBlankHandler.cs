using Application.Handler.InterfaceHandler;
using Application.Models.Exam;
using Domain.Entity;

namespace Application.Handler
{
    public class FillInBlankHandler:IQuestionTypesHandler
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
            var isCorrectFill = examDetail
                                .Question.Answers
                                .Any(a => string.Equals(a.Content, userAnswer.AnswerFillInBlank, StringComparison.OrdinalIgnoreCase));
            if (isCorrectFill)
            {
                return new AnswerHistory
                {
                    Id = Guid.NewGuid(),
                    ExamResultId = examResultId,
                    QuestionId = examDetail.QuestionId,
                    UserAnswer = userAnswer?.AnswerFillInBlank ?? "",
                    IsCorrect = true,
                    Score = isCorrectFill ? examDetail.Score : 0,
                };
            }
            return new AnswerHistory
            {
                Id = Guid.NewGuid(),
                ExamResultId = examResultId,
                QuestionId = examDetail.QuestionId,
                UserAnswer = userAnswer?.AnswerFillInBlank ?? "",
                IsCorrect = false,
                Score = 0,
            };
        }
        public double CalculateScoreHandler(UserAnswerRequest userAnswer, ExamDetail examDetail)
        {
            if(userAnswer == null) return 0;
            if(string.IsNullOrWhiteSpace(userAnswer.AnswerFillInBlank)) return 0;
            var correctAnswer = examDetail.Question.Answers
                                .FirstOrDefault(a => a.IsCorrect == true) ?? throw new Exception("This question has no correct answer");
            if (userAnswer.AnswerFillInBlank == correctAnswer.Content) return examDetail.Score;
            return 0;
        }
    }
}
