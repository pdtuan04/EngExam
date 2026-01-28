using Application.DTOs.Requests.Exam;
using Application.DTOs.Requests.Question;
using Application.DTOs.Responses;
using Application.Exceptions;
using Application.Handler.InterfaceHandler;
using Application.Interface.Exam;
using Application.Repositories;
using Application.UnitOfWork;
using Domain.Entity;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class SubmitExam : ISubmitExam
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetExamFinder _getExamFinder;
        private readonly IDictionary<QuestionTypes, IQuestionTypesHandler> _questionHandlers;
        public SubmitExam(
            IUnitOfWork submitExamUnitOfWork,
            IGetExamFinder getExamFinder,
            IDictionary<QuestionTypes, IQuestionTypesHandler> questionHandlers)
        {
            _unitOfWork = submitExamUnitOfWork ?? throw new ArgumentNullException(nameof(submitExamUnitOfWork));
            _getExamFinder = getExamFinder;
            _questionHandlers = questionHandlers;
        }
        public async Task<ExamResultDto> SubmitExamAsync(SubmitExamRequest submit, Guid userId)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {

                var now = DateTime.UtcNow;
                var exam = await _unitOfWork.ExamRepository.GetByIdAsync(submit.ExamId) ?? throw new ExamNullException();
                var score = ScoreCalculation(submit.UserAnswers,exam.ExamDetail);
                var histories = HistorySave(submit.UserAnswers, exam.ExamDetail, exam.Id);
                var examResultId = Guid.NewGuid();
                var examResult = new ExamResult
                {
                    Id = examResultId,
                    ExamId = exam.Id,
                    UserId = userId,
                    Score = score,
                    CompleteAt = now,
                    AnswerHistory = histories,
                };
                await _unitOfWork.ExamResultRepository.AddAsync(examResult);
                await _unitOfWork.SaveChangesAsync();
                var examResultDto = new ExamResultDto
                {
                    Id = examResult.Id,
                    CompleteAt = examResult.CompleteAt,
                    TotalScore = examResult.Score,
                    Details = exam
                                .ExamDetail
                                .Select(ed =>
                                {
                                    var history = histories.First(h => h.QuestionId == ed.QuestionId);
                                    return new UserAnswerDto
                                    {
                                        Content = ed.Question.Content,
                                        QuestionTypes = ed.Question.QuestionTypes,
                                        UserAnswer = history.UserAnswer,
                                        Explanation = ed.Question.Explanation,
                                        IsCorrect = history.IsCorrect,
                                        EarnedPoint = history.Score,
                                        Options = ed.Question.Answers.Select(a => new Option
                                        {
                                            Content = a.Content,
                                            IsCorrect = a.IsCorrect,
                                        }).ToList()
                                    };
                                }).ToList()
                };
                return examResultDto;
            }
            catch
            {
                await _unitOfWork.CancelAsync();
                throw;
            }
        }
        public double ScoreCalculation(ICollection<UserAnswer> userAnswers, ICollection<ExamDetail> examDetails)
        {
            double score = 0;
            foreach(var ed in examDetails)
            {
                var userAnswer = userAnswers.FirstOrDefault(ua => ua.QuestionId == ed.QuestionId);
                _questionHandlers.TryGetValue(ed.Question.QuestionTypes, out var handler);
                score += handler.CalculateScoreHandler(userAnswer, ed);
            }
            return score;
        }
        public ICollection<AnswerHistory> HistorySave(ICollection<UserAnswer> userAnswers, ICollection<ExamDetail> examDetails, Guid examResultId)
        {
            var answerHistories = new List<AnswerHistory>();
            foreach (var ed in examDetails)
            {
                var userAnswer = userAnswers.FirstOrDefault(ua => ua.QuestionId == ed.QuestionId);
                _questionHandlers.TryGetValue(ed.Question.QuestionTypes, out var handler);
                var answerHistory = handler.HistoryHandler(userAnswer, ed,examResultId);
                answerHistories.Add(answerHistory);
            }
            return answerHistories;
        }
    }
}
