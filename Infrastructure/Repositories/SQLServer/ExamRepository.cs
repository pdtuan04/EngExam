using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Repositories;
using Application.Common;
using Application.Common.Exceptions;
using AutoMapper;
using Infrastructure.Common;
using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SQLServer
{
    public class ExamRepository : GenericRepository<Domain.Entity.Exam, Exam, Guid>, IExamRepository
    {
        public ExamRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public async Task<IEnumerable<Domain.Entity.Exam>> GetAllAsync()
        {
            var dbExams = await _dbContext.Exams.ToListAsync();
            return _mapper.Map<IEnumerable<Domain.Entity.Exam>>(dbExams);
        }

        public async Task<Domain.Entity.Exam> GetRandomExam()
        {
            var randomExam = await _dbContext.Exams
                .AsNoTracking()
                .Include(e => e.ExamDetail)
                .ThenInclude(ed => ed.Question)
                .ThenInclude(q => q.Answers)
                .OrderBy(x => Guid.NewGuid())
                .FirstOrDefaultAsync(e => e.IsActive == true);
            return _mapper.Map<Domain.Entity.Exam>(randomExam);
        }
        public async Task<IEnumerable<Domain.Entity.Exam>> GetExamsByCategoryIdAsync(Guid categoryId)
        {
            var dbExams = await _dbContext.Exams
                .Where(e => e.ExamCategoryId == categoryId && e.IsActive == true)
                .ToListAsync();
            return _mapper.Map<IEnumerable<Domain.Entity.Exam>>(dbExams);
        }

        public async Task<Domain.Entity.Exam> GetExamToTake(Guid id)
        {
            var dbExam = await _dbContext.Exams
                .AsNoTracking()
                .Include(e => e.ExamDetail)
                .ThenInclude(ed => ed.Question)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive == true);
            return _mapper.Map<Domain.Entity.Exam>(dbExam);
        }
        public async Task<Domain.Entity.Exam> GetExamDetail(Guid id)
        {
            var dbExam = await _dbContext.Exams
                .AsNoTracking()
                .Include(e => e.ExamDetail)
                .ThenInclude(ed => ed.Question)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<Domain.Entity.Exam>(dbExam);
        }
        public async Task<bool> SoftDelete(Guid id)
        {
            var dbExam = await _dbContext.Exams.FirstOrDefaultAsync(e => e.Id == id) ?? throw new NullReferenceException();
            dbExam.IsActive = false;
            return true;
        }
        public override async Task Update(Domain.Entity.Exam exam)
        {
            var dbExam = await _dbContext.Exams
                .Include(e => e.ExamDetail)
                .ThenInclude(ed => ed.Question)
                .ThenInclude(q => q.Answers)
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.Id == exam.Id);
            if (dbExam == null) return;
            var dbExamDetails = dbExam.ExamDetail.ToDictionary(ed => ed.QuestionId, ed => ed);
            var questions = exam.ExamDetail.Select(ed => ed.Question).ToList();
            var newScore = exam.ExamDetail.ToDictionary(ed => ed.QuestionId, ed => ed.Score);
            var newQuestionIds = exam.ExamDetail.Select(ed => ed.QuestionId).ToHashSet();
            var existingQuestionIds = dbExamDetails.Keys.ToHashSet();
            var existingAnswers = dbExam.ExamDetail.SelectMany(ed => ed.Question.Answers).ToDictionary(a => a.Id, a => a);
            dbExam.Title = exam.Title;
            dbExam.Description = exam.Description;
            dbExam.IsActive = exam.IsActive;
            dbExam.DurationInMinutes = exam.DurationInMinutes;
            dbExam.UpdatedAt = exam.UpdatedAt;
            dbExam.ExamCategoryId = exam.ExamCategoryId;
            foreach (var question in questions)
            {
                if (dbExamDetails.TryGetValue(question.Id, out var existingQuestion))
                {
                    existingQuestion.Question.Content = question.Content;
                    existingQuestion.Question.QuestionTypes = question.QuestionTypes;
                    existingQuestion.Question.Explanation = question.Explanation;
                    existingQuestion.Question.ImageUrl = question.ImageUrl;
                    existingQuestion.Question.TopicId = question.TopicId;
                    existingQuestion.Question.IsActive = question.IsActive;
                    existingQuestion.Question.UpdatedAt = exam.UpdatedAt;
                    existingQuestion.Score = newScore.TryGetValue(question.Id, out var score) ? score : existingQuestion.Score;
                    foreach (var answer in question.Answers)
                    {
                        
                        if (existingAnswers.TryGetValue(answer.Id, out var existingAnswer))
                        {
                            existingAnswer.Content = answer.Content;
                            existingAnswer.IsCorrect = answer.IsCorrect;
                            existingAnswer.UpdatedAt = exam.UpdatedAt;
                        }
                        else
                        {
                            var newAnswer = new Answer
                            {
                                Id = answer.Id,
                                Content = answer.Content,
                                IsCorrect = answer.IsCorrect,
                                QuestionId = question.Id,
                                CreatedAt = exam.UpdatedAt,
                                UpdatedAt = exam.UpdatedAt
                            };
                            existingQuestion.Question.Answers.Add(newAnswer);
                        }
                    }
                }
                else
                {
                    var newQuestion = new Question
                    {
                        Id = question.Id,
                        Content = question.Content,
                        QuestionTypes = question.QuestionTypes,
                        Explanation = question.Explanation,
                        ImageUrl = question.ImageUrl,
                        TopicId = question.TopicId,
                        CreatedAt = exam.UpdatedAt,
                        UpdatedAt = exam.UpdatedAt,
                        Answers = new List<Answer>()
                    };
                    foreach (var answer in question.Answers)
                    {
                        var newAnswer = new Answer
                        {
                            Id = answer.Id,
                            Content = answer.Content,
                            IsCorrect = answer.IsCorrect,
                            QuestionId = question.Id,
                            CreatedAt = exam.UpdatedAt,
                            UpdatedAt = exam.UpdatedAt,

                        };
                        newQuestion.Answers.Add(newAnswer);
                    }
                    _dbContext.Questions.Add(newQuestion);
                    dbExam.ExamDetail.Add(new ExamDetail
                    {
                        ExamId = exam.Id,
                        QuestionId = question.Id,
                        Score = newScore.TryGetValue(question.Id, out var score) ? score : 0,
                        Question = newQuestion
                    });
                }
            }
            foreach (var questionId in existingQuestionIds)
            {
                if (!newQuestionIds.Contains(questionId))
                {
                    var examDetail = dbExamDetails[questionId];
                    dbExam.ExamDetail.Remove(examDetail);
                    _dbContext.ExamDetails.Remove(examDetail);
                }
            }
            var incomingAnswerIds = exam.ExamDetail.SelectMany(ed => ed.Question.Answers).Select(a => a.Id).ToHashSet();
            var removesAnswers = dbExam.ExamDetail
                .SelectMany(ed => ed.Question?.Answers ?? new List<Answer>())
                .Where(a => !incomingAnswerIds.Contains(a.Id)).ToList();
            foreach (var removeAnswer in removesAnswers)
            {
                removeAnswer.IsDeleted = true;
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
