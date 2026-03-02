using Application.Common.Interfaces;
using Application.Exceptions;
using Application.Handler.InterfaceHandler;
using Application.Models.Answer;
using Application.Models.Exam;
using Application.Models.ExamCategory;
using Application.Models.ExamResult;
using Application.Models.Pagination;
using Application.Models.Question;
using Domain.Entity;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ExamService : IExamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDictionary<QuestionTypes, IQuestionTypesHandler> _questionHandlers;
        public ExamService(IUnitOfWork unitOfWork, IDictionary<QuestionTypes, IQuestionTypesHandler> questionHandlers)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _questionHandlers = questionHandlers;
        }
        public async Task<ExamDetailResponse> Create(CreateExamRequest request)
        {
            await _unitOfWork.BeginTransactionAsync();
            var now = DateTime.UtcNow;
            var exam = new Exam
            {
                Id = Guid.NewGuid(),
                IsActive = true,
                CreatedAt = now,
                Title = request.Title,
                DurationInMinutes = request.DurationInMinutes,
                ExamCategoryId = request.ExamCategoryId,
            };

            foreach (var q in request.Questions)
            {
                var questionId = Guid.NewGuid();
                exam.AddExamDetail(new Question
                {
                    Id = questionId,
                    IsActive = true,
                    Content = q.Content,
                    Explanation = q.Explanation,
                    QuestionTypes = q.QuestionTypes,
                    TopicId = q.TopicId,
                    CreatedAt = now,
                    Answers = q.Answers.Select(a => new Answer
                    {
                        Id = Guid.NewGuid(),
                        IsActive = true,
                        CreatedAt = now,
                        Content = a.Content,
                        IsCorrect = a.IsCorrect,
                        QuestionId = questionId,
                    }).ToList(),
                },
                q.Score);
            }
            await _unitOfWork.ExamRepository.AddAsync(exam);
            await _unitOfWork.SaveChangesAsync();
            return new ExamDetailResponse
            {
                Id = exam.Id,
                Title = exam.Title,
                DurationInMinutes = exam.DurationInMinutes,
                ExamCategoryId = exam.ExamCategoryId,
                CreatedAt = exam.CreatedAt,
                Questions = exam.ExamDetail.Select(ed => new QuestionDetailResponse
                {
                    Id = ed.Question.Id,
                    Content = ed.Question.Content,
                    Explanation = ed.Question.Explanation ?? "",
                    QuestionTypes = ed.Question.QuestionTypes,
                    TopicId = ed.Question.TopicId,
                    Score = ed.Score,
                    CreateAt = ed.Question.CreatedAt,
                    Answers = ed.Question.Answers.Select(a => new AnswerDetailsResponse
                    {
                        Id = a.Id,
                        Content = a.Content,
                        IsCorrect = a.IsCorrect,
                        QuestionId = a.QuestionId,
                    }).ToList(),
                }).ToList(),
            };
        }

        public async Task<ExamResponse> GetById(Guid id)
        {
            var result = await _unitOfWork.ExamRepository.GetByIdAsync(id);
            return new ExamResponse
            {
                Id = result.Id,
                Title = result.Title,
                DurationInMinutes = result.DurationInMinutes,
                ExamCategoryId = result.ExamCategoryId,
                CreatedAt = result.CreatedAt,
            };
        }

        public async Task<IEnumerable<ExamResponse>?> GetExamsByCategoryIdAsync(Guid id)
        {
            var result = await _unitOfWork.ExamRepository.GetExamsByCategoryIdAsync(id);
            return result.Select(e => new ExamResponse
            {
                Id = e.Id,
                Title = e.Title,
                DurationInMinutes = e.DurationInMinutes,
                ExamCategoryId = e.ExamCategoryId,
                CreatedAt = e.CreatedAt,
            }).ToList();
        }

        public async Task<TakeExamResponse> GetExamToTake(Guid id)
        {
            var result = await _unitOfWork.ExamRepository.GetExamToTake(id);
            return new TakeExamResponse
            {
                Id = result.Id,
                Title = result.Title,
                DurationInMinutes = result.DurationInMinutes,
                Questions = result.ExamDetail.Select(ed => new QuestionToTakeResponse
                {
                    Id = ed.Question.Id,
                    Content = ed.Question.Content,
                    QuestionTypes = ed.Question.QuestionTypes,
                    Answers = ed.Question.Answers.Select(a => new AnswerToTakeResponse
                    {
                        Id = a.Id,
                        Content = a.Content,
                    }).ToList(),
                }).ToList(),
            };
        }

        public async Task<TakeExamResponse> GetRandomExamToTake()
        {
            var result = await _unitOfWork.ExamRepository.GetRandomExam();
            return new TakeExamResponse
            {
                Id = result.Id,
                Title = result.Title,
                DurationInMinutes = result.DurationInMinutes,
                Questions = result.ExamDetail.Select(ed => new QuestionToTakeResponse
                {
                    Id = ed.Question.Id,
                    Content = ed.Question.Content,
                    QuestionTypes = ed.Question.QuestionTypes,
                    Answers = ed.Question.Answers.Select(a => new AnswerToTakeResponse
                    {
                        Id = a.Id,
                        Content = a.Content,
                    }).ToList(),
                }).ToList(),
            };
        }
        private async Task<double> ScoreCalculation(ICollection<UserAnswerRequest> userAnswers, ICollection<ExamDetail> examDetails)
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
        private async Task<ICollection<AnswerHistory>> HistorySave(ICollection<UserAnswerRequest> userAnswers, ICollection<ExamDetail> examDetails, Guid examResultId)
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
        public async Task<ExamResultDetailResponse> SubmitExam(Guid userId, SubmitExamRequest request)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {

                var now = DateTime.UtcNow;
                var exam = await _unitOfWork.ExamRepository.GetExamToTake(request.ExamId) ?? throw new ExamNullException();
                var score = await ScoreCalculation(request.UserAnswers, exam.ExamDetail);
                var histories = await HistorySave(request.UserAnswers, exam.ExamDetail, exam.Id);
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
                var examResultDto = new ExamResultDetailResponse
                {
                    Id = examResult.Id,
                    CompleteAt = examResult.CompleteAt,
                    TotalScore = examResult.Score,
                    UserAnswers = exam
                                .ExamDetail
                                .Select(ed =>
                                {
                                    var history = histories.First(h => h.QuestionId == ed.QuestionId);
                                    return new UserAnswerResponse
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

        public async Task<PaginationResponse<ExamResponse>> GetPaginated(PaginatedRequest request)
        {
            return await _unitOfWork.ExamRepository.ToPagination(
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                orderBy: x => x.Title,
                ascending: true,
                selector: x => new ExamResponse
                {
                    Id = x.Id,
                    Title= x.Title,
                    Description = x.Description,
                    ExamCategoryId = x.ExamCategoryId,
                    DurationInMinutes = x.DurationInMinutes,
                    CreatedAt = x.CreatedAt,
                }
            );
        }

        public async Task<ExamDetailResponse> Update(UpdateExamRequest request)
        {
            await _unitOfWork.BeginTransactionAsync();
            var now = DateTime.UtcNow;
            var exam = await _unitOfWork.ExamRepository.GetExamDetail(request.Id) ?? throw new ExamNullException();
            exam.IsActive = request.IsActive;
            exam.Title = request.Title;
            exam.Description = request.Description;
            exam.DurationInMinutes = request.DurationInMinutes;
            exam.ExamCategoryId = request.ExamCategoryId;
            exam.UpdatedAt = now;
            foreach (var q in request.Questions)
            {
                var existQues = exam.ExamDetail.FirstOrDefault(ed => ed.QuestionId == q.Id);
                if (existQues == null)
                {
                    exam.AddExamDetail(new Question
                    {
                        Id = q.Id,
                        Content = q.Content,
                        Explanation = q.Explanation,
                        TopicId = q.TopicId,
                        QuestionTypes = q.QuestionTypes,
                        ImageUrl = q.ImageUrl,
                        IsActive = q.IsActive,
                    }, q.Score);
                }
                else
                {
                    existQues.Question.Content = q.Content;
                    existQues.Question.Explanation = q.Explanation;
                    existQues.Question.TopicId = q.TopicId;
                    existQues.Question.QuestionTypes = q.QuestionTypes;
                    existQues.Question.ImageUrl = q.ImageUrl;
                    existQues.Question.IsActive = q.IsActive;
                    existQues.Score = q.Score;
                    foreach (var a in q.Answers)
                    {
                        var existAns = existQues.Question.Answers.FirstOrDefault(ans => ans.Id == a.Id);
                        if (existAns == null)
                        {
                            existQues.Question.Answers.Add(new Answer
                            {
                                Id = a.Id,
                                Content = a.Content,
                                IsCorrect = a.IsCorrect,
                                QuestionId = q.Id,
                                IsActive = true,
                            });
                        }
                        else
                        {
                            existAns.Content = a.Content;
                            existAns.IsCorrect = a.IsCorrect;
                            existAns.IsActive = true;
                        }
                    }
                }
                
            }
            await _unitOfWork.ExamRepository.Update(exam);
            await _unitOfWork.SaveChangesAsync();
            return new ExamDetailResponse
            {
                Id = exam.Id,
                Title = exam.Title,
                Description = exam.Description,
                DurationInMinutes = exam.DurationInMinutes,
                ExamCategoryId = exam.ExamCategoryId,
                CreatedAt = exam.CreatedAt,
                Questions = exam.ExamDetail.Select(ed => new QuestionDetailResponse
                {
                    Id = ed.Question.Id,
                    Content = ed.Question.Content,
                    Explanation = ed.Question.Explanation ?? "",
                    QuestionTypes = ed.Question.QuestionTypes,
                    TopicId = ed.Question.TopicId,
                    Score = ed.Score,
                    CreateAt = ed.Question.CreatedAt,
                    Answers = ed.Question.Answers.Select(a => new AnswerDetailsResponse
                    {
                        Id = a.Id,
                        Content = a.Content,
                        IsCorrect = a.IsCorrect,
                        QuestionId = a.QuestionId,
                    }).ToList(),
                }).ToList(),
            };
        }

        public async Task<bool> Delete(Guid id)
        {
            await _unitOfWork.ExamRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDelete(Guid id)
        {
            await _unitOfWork.ExamRepository.SoftDelete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
