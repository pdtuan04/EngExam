using Application.Abstractions.Messaging;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Handler.InterfaceHandler;
using Application.Models.Exam;
using Application.Models.ExamResult;
using Domain.Entity;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamResult.Commands
{
    public sealed class SaveExamResultCommandHandler : ICommandHandler<SaveExamResultCommand, ExamResultDetailResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDictionary<QuestionTypes, IQuestionTypesHandler> _questionHandlers;
        public SaveExamResultCommandHandler(IUnitOfWork unitOfWork, IDictionary<QuestionTypes, IQuestionTypesHandler> questionHandlers)
        {
            _unitOfWork = unitOfWork;
            _questionHandlers = questionHandlers;
        }
        public async Task<ExamResultDetailResponse> Handle(SaveExamResultCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var exam = await _unitOfWork.ExamRepository.GetExamToTake(request.ExamId) ?? throw new NotFoundException("Exam", request.ExamId);
            var score = await ScoreCalculation(request.UserAnswers, exam.ExamDetail);
            var histories = await HistorySave(request.UserAnswers, exam.ExamDetail, exam.Id);
            var examResultId = Guid.NewGuid();
            var examResult = new Domain.Entity.ExamResult
            {
                Id = examResultId,
                ExamId = exam.Id,
                UserId = request.UserId,
                Score = score,
                CompleteAt = now,
                AnswerHistory = histories
            };
            await _unitOfWork.ExamResultRepository.AddAsync(examResult);
            await _unitOfWork.SaveChangesAsync();
            var examResultDto = new ExamResultDetailResponse
            (
                Id: examResult.Id,
                CompleteAt: examResult.CompleteAt,
                TotalScore: examResult.Score,
                UserAnswers: exam
                            .ExamDetail
                            .Select(ed =>
                            {
                                var history = histories.First(h => h.QuestionId == ed.QuestionId);
                                return new UserAnswerResponse
                                (
                                    Content: ed.Question.Content,
                                    QuestionTypes: ed.Question.QuestionTypes,
                                    UserAnswer: history.UserAnswer,
                                    Explanation: ed.Question.Explanation,
                                    IsCorrect: history.IsCorrect,
                                    EarnedPoint: history.Score,
                                    Options: ed.Question.Answers.Select(a => new Option
                                    (
                                        Content: a.Content,
                                        IsCorrect: a.IsCorrect
                                    )).ToList()
                                );
                            }).ToList()
            );
            return examResultDto;
        }
        private async Task<double> ScoreCalculation(IReadOnlyCollection<UserAnswerRequest> userAnswers, ICollection<ExamDetail> examDetails)
        {
            double score = 0;
            foreach (var ed in examDetails)
            {
                var userAnswer = userAnswers.FirstOrDefault(ua => ua.QuestionId == ed.QuestionId);
                _questionHandlers.TryGetValue(ed.Question.QuestionTypes, out var handler);
                score += handler.CalculateScoreHandler(userAnswer, ed);
            }
            return score;
        }
        private async Task<ICollection<AnswerHistory>> HistorySave(IReadOnlyCollection<UserAnswerRequest> userAnswers, ICollection<ExamDetail> examDetails, Guid examResultId)
        {
            var answerHistories = new List<AnswerHistory>();
            foreach (var ed in examDetails)
            {
                var userAnswer = userAnswers.FirstOrDefault(ua => ua.QuestionId == ed.QuestionId);
                _questionHandlers.TryGetValue(ed.Question.QuestionTypes, out var handler);
                var answerHistory = handler.HistoryHandler(userAnswer, ed, examResultId);
                answerHistories.Add(answerHistory);
            }
            return answerHistories;
        }

    }
}
