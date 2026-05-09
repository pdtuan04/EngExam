using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Models.Answer;
using Application.Models.Exam;
using Application.Models.ExamResult;
using Application.Models.Pagination;
using Application.Models.Practice;
using Application.Models.Question;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCore.BulkExtensions;
using Infrastructure.Common;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public class PracticeReadRepository : IPracticeReadRepository
    {
        private readonly ApplicationDbReadContext _dbContext;
        private readonly IMapper _mapper;

        public PracticeReadRepository(ApplicationDbReadContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }


        public async Task<PaginationResponse<PracticeResponse>> GetPracticePaginatedByTopicIdAsync(Guid topicId, int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var query = _dbContext.Practices
                .AsNoTracking()
                .Where(p => p.TopicId == topicId);
            var projectedQuery = query.ProjectTo<PracticeResponse>(_mapper.ConfigurationProvider);
            var queryExecute = await PaginationDb<PracticeResponse>.ToPagedList(projectedQuery, pageIndex, pageSize);
            return new PaginationResponse<PracticeResponse>(queryExecute.Items, queryExecute.TotalCount, pageIndex, pageSize);
        }

        public async Task<PracticeDetailResponse> GetPracticeToTake(Guid id)
        {
            var practice = await _dbContext.Practices.FindAsync(id);
            if (practice == null) return null;
            var questions = await _dbContext.PracticeDetails
                .Where(pd => pd.PracticeId == id)
                .Join(_dbContext.Questions, pd => pd.QuestionId, q => q.Id,
                (pd, q) => new 
                {
                    q.Id,
                    q.Content,
                    q.QuestionTypes,
                    q.Explanation,
                    q.ImageUrl,
                    q.CreatedAt,
                    q.UpdatedAt,
                    q.TopicId,
                }).ToListAsync();
            var quesitonsId = questions.Select(q => q.Id).ToHashSet();
            var answers = await _dbContext.Answers.Where(a => quesitonsId.Contains(a.QuestionId)).ToListAsync();
            return new PracticeDetailResponse(
                Id: practice.Id,
                Title: practice.Title,
                Description: practice.Description,
                CreatedAt: practice.CreatedAt,
                TopicId: practice.TopicId,
                Questions: questions.Select(q => new QuestionToPracticeResponse(
                    q.Id,
                    q.Content,
                    q.QuestionTypes,
                    q.Explanation,
                    q.ImageUrl,
                    Answers: answers
                        .Where(a => a.QuestionId == q.Id)
                        .Select(a => new AnswerToPracticeResponse(
                            a.Id,
                            a.Content,
                            a.IsCorrect
                            )).ToList()
                )).ToList()
            );
        }

        public async Task UpsertAsync(PracticeReadModel practice)
        {
            var existingPractice = await _dbContext.Practices.IgnoreQueryFilters().AsTracking().FirstOrDefaultAsync(p => p.Id == practice.Id);
            if (existingPractice != null)
            {
                if (existingPractice.UpdatedAt >= practice.UpdatedAt)
                {
                    return;
                }
                _mapper.Map(practice, existingPractice);
            }
            else
            {
                var newPractice = _mapper.Map<Practice>(practice);
                newPractice.IsDeleted = false;
                _dbContext.Practices.Add(newPractice);
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid practiceId, DateTime deletedAt)
        {
            var practice = await _dbContext.Practices
            .IgnoreQueryFilters()
            .AsTracking()
            .FirstOrDefaultAsync(p => p.Id == practiceId);
            if (practice != null)
            {
                if (practice.UpdatedAt >= deletedAt)
                {
                    return;
                }
                practice.IsDeleted = true;
                practice.UpdatedAt = deletedAt;
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task UpsertPracticeDetailsAsync(IEnumerable<PracticeDetailReadModel> details)
        {
            if (details == null || !details.Any()) return;
            var entities = _mapper.Map<List<PracticeDetail>>(details);
            var incomingQuestionIds = details.Select(d => d.QuestionId).ToHashSet();
            var existingDetails = await _dbContext.PracticeDetails.Where(ed => ed.PracticeId == details.First().PracticeId && !incomingQuestionIds.Contains(ed.QuestionId))
                .ToListAsync();
            _dbContext.PracticeDetails.RemoveRange(existingDetails);
            await _dbContext.SaveChangesAsync();
            await _dbContext.BulkInsertOrUpdateAsync(entities);
        }
    }
}
