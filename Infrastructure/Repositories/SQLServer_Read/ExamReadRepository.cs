using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Models.Answer;
using Application.Models.Exam;
using Application.Models.Pagination;
using Application.Models.Question;
using Application.Models.Topic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore.BulkExtensions;
using Infrastructure.Common;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public class ExamReadRepository : IExamReadRepository
    {
        private readonly ApplicationDbReadContext _dbContext;
        private readonly IMapper _mapper;

        public ExamReadRepository(ApplicationDbReadContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }
        public async Task<IEnumerable<ExamResponse>> GetAllAsync()
        {
            var dbExams = await _dbContext.Exams.ToListAsync();
            return _mapper.Map<IEnumerable<ExamResponse>>(dbExams);
        }

        public async Task<TakeExamResponse> GetRandomExam()
        {
            var count = await _dbContext.Exams.CountAsync();

            var random = new Random();
            var index = random.Next(count);

            var dbExam = await _dbContext.Exams
                .Skip(index)
                .FirstOrDefaultAsync();
            if (dbExam == null) return null;
            var questions = await _dbContext.ExamDetails
                .Where(ed => ed.ExamId == dbExam.Id)
                .Join(_dbContext.Questions, ed => ed.QuestionId, q => q.Id, (ed, q) => new
                {
                    q.Id,
                    q.Content,
                    q.QuestionTypes,
                    q.ImageUrl,
                    q.CreatedAt,
                    q.UpdatedAt,
                    q.TopicId,
                    ed.Score
                }).ToListAsync();
            var questionIds = questions.Select(q => q.Id).ToList();
            var answers = await _dbContext.Answers
                .Where(a => questionIds.Contains(a.QuestionId))
                .ToListAsync();
            return new TakeExamResponse(
                Id: dbExam.Id,
                Title: dbExam.Title,
                Description: dbExam.Description,
                DurationInMinutes: dbExam.DurationInMinutes,
                Questions: questions.Select(q => new QuestionToTakeResponse(
                    Id: q.Id,
                    Content: q.Content,
                    QuestionTypes: q.QuestionTypes,
                    Answers: answers
                        .Where(a => a.QuestionId == q.Id)
                        .Select(a => new AnswerToTakeResponse(
                            Id: a.Id,
                            Content: a.Content
                        )).ToList()
                )).ToList()
            );
        }
        public async Task<IEnumerable<ExamResponse>> GetExamsByCategoryIdAsync(Guid categoryId)
        {
            var dbExams = await _dbContext.Exams
                .Where(e => e.ExamCategoryId == categoryId && e.IsActive == true)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ExamResponse>>(dbExams);
        }

        public async Task<TakeExamResponse> GetExamToTake(Guid id)
        {
            var dbExam = await _dbContext.Exams.FirstOrDefaultAsync(e => e.Id == id);
            if (dbExam == null) return null;
            var questions = await _dbContext.ExamDetails
                .Where(ed => ed.ExamId == id)
                .Join(_dbContext.Questions, ed => ed.QuestionId, q => q.Id, (ed, q) => new
                {
                    q.Id,
                    q.Content,
                    q.QuestionTypes,
                    q.ImageUrl,
                    q.CreatedAt,
                    q.UpdatedAt,
                    q.TopicId,
                    ed.Score
                }).ToListAsync();
            var questionIds = questions.Select(q => q.Id).ToHashSet();
            var answers = await _dbContext.Answers
                .Where(a => questionIds.Contains(a.QuestionId))
                .ToListAsync();
            return new TakeExamResponse(
                Id: dbExam.Id,
                Title: dbExam.Title,
                Description: dbExam.Description,
                DurationInMinutes: dbExam.DurationInMinutes,
                Questions: questions.Select(q => new QuestionToTakeResponse(
                    Id: q.Id,
                    Content: q.Content,
                    QuestionTypes: q.QuestionTypes,
                    Answers: answers
                        .Where(a => a.QuestionId == q.Id)
                        .Select(a => new AnswerToTakeResponse(
                            Id: a.Id,
                            Content: a.Content
                        )).ToList()
                )).ToList()
            );
        }
        public async Task<ExamDetailResponse> GetExamDetail(Guid id)
        {
            var dbExam = await _dbContext.Exams.FirstOrDefaultAsync(e => e.Id == id);
            if (dbExam == null) return null;
            var questions = await _dbContext.ExamDetails
                .Where(ed => ed.ExamId == id)
                .Join(_dbContext.Questions, ed => ed.QuestionId, q => q.Id, (ed, q) => new
                {
                    q.Id,
                    q.Content,
                    q.QuestionTypes,
                    q.Explanation,
                    q.ImageUrl,
                    q.CreatedAt,
                    q.UpdatedAt,
                    q.TopicId,
                    ed.Score
                }).ToListAsync();
            var questionIds = questions.Select(q => q.Id).ToHashSet();
            var answers = await _dbContext.Answers
                .Where(a => questionIds.Contains(a.QuestionId))
                .ToListAsync();
            return new ExamDetailResponse(
                Id: dbExam.Id,
                Title: dbExam.Title,
                Description: dbExam.Description,
                DurationInMinutes: dbExam.DurationInMinutes,
                CreatedAt: dbExam.CreatedAt,
                ExamCategoryId: dbExam.ExamCategoryId,
                Questions: questions.Select(q => new QuestionDetailResponse(
                    Id: q.Id,
                    Content: q.Content,
                    QuestionTypes: q.QuestionTypes,
                    CreateAt: q.CreatedAt,
                    Explanation: q.Explanation,
                    Score: q.Score,
                    ImageUrl: q.ImageUrl,
                    TopicId: q.TopicId,
                    Answers: answers
                        .Where(a => a.QuestionId == q.Id)
                        .Select(a => new AnswerDetailsResponse(
                            Id: a.Id,
                            Content: a.Content,
                            IsCorrect: a.IsCorrect,
                            QuestionId: a.QuestionId
                        )).ToList()
                )).ToList()
            );
        }
        public async Task UpsertAsync(ExamReadModel exam)
        {
            var existingExam = await _dbContext.Exams
                                            .AsTracking()
                                            .FirstOrDefaultAsync(x => x.Id == exam.Id);
            if (existingExam != null)
            {
                if(existingExam.UpdatedAt >= exam.UpdatedAt) return;
                _mapper.Map(exam, existingExam);
            }
            else
            {
                var newExam = _mapper.Map<Exam>(exam);
                await _dbContext.Exams.AddAsync(newExam);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id, DateTime deletedAt)
        {
            var exam = await _dbContext.Exams
            .IgnoreQueryFilters()
            .AsTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
            if (exam != null)
            {
                if (exam.UpdatedAt >= deletedAt)
                {
                    return;
                }
                exam.IsDeleted = true;
                exam.UpdatedAt = deletedAt;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<PaginationResponse<ExamResponse>> GetPaginatedAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var query = _dbContext.Exams.AsNoTracking();
            var projectedQuery = query.ProjectTo<ExamResponse>(_mapper.ConfigurationProvider);
            var queryExecute = await PaginationDb<ExamResponse>.ToPagedList(projectedQuery, page, pageSize);
            return new PaginationResponse<ExamResponse>(queryExecute.Items, queryExecute.TotalCount, page, pageSize);
        }

        public async Task UpsertExamDetailsAsync(IEnumerable<ExamDetailReadModel> details, Guid examId)
        {
            if (details == null || !details.Any()) return;
            var entities = _mapper.Map<List<ExamDetail>>(details);
            var incomingQuestionIds = details.Select(d => d.QuestionId).ToHashSet();
            var existingDetails = await _dbContext.ExamDetails.Where(ed => ed.ExamId == examId && !incomingQuestionIds.Contains(ed.QuestionId))
                .ToListAsync();
            _dbContext.ExamDetails.RemoveRange(existingDetails);
            await _dbContext.SaveChangesAsync();
            await _dbContext.BulkInsertOrUpdateAsync(entities);
            
        }
    }
}