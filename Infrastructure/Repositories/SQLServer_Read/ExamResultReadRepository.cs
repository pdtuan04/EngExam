using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Models.ExamResult;
using Application.Models.Pagination;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Common;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public class ExamResultReadRepository : IExamResultReadRepository
    {
        private readonly ApplicationDbReadContext _dbContext;
        private readonly IMapper _mapper;
        private readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
        };
        public ExamResultReadRepository(ApplicationDbReadContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public async Task<ExamResultDetailResponse?> GetByIdAsync(Guid id)
        {
            var dbexamresult = await _dbContext.ExamResults.FindAsync(id);
            return dbexamresult is null ? null : _mapper.Map<ExamResultDetailResponse>(dbexamresult);
        }
        public async Task<IEnumerable<ExamResultResponse>> GetAllAsync()
        {
            var dbexamresults = await _dbContext.ExamResults.AsNoTracking().ToListAsync();
            return _mapper.Map<List<ExamResultResponse>>(dbexamresults);
        }
        public async Task<IEnumerable<ExamResultResponse>> GetResultsByUserId(Guid id)
        {
            var dbexamresults = await _dbContext.ExamResults
                .AsNoTracking()
                .Where(er => er.UserId == id)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ExamResultResponse>>(dbexamresults);
        }
        public async Task<PaginationResponse<ExamResultResponse>> GetExamResultPaginatedByUserId(Guid userId, int pageIndex, int pageSize,CancellationToken cancellationToken)
        {
            var query = _dbContext.ExamResults
                                .AsNoTracking()
                                .Where(x => x.UserId == userId);
            var projectedQuery = query.ProjectTo<ExamResultResponse>(_mapper.ConfigurationProvider);
            var queryExecute = await PaginationDb<ExamResultResponse>.ToPagedList(projectedQuery, pageIndex, pageSize);

            return new PaginationResponse<ExamResultResponse>(queryExecute.Items, queryExecute.TotalCount, pageIndex, pageSize);
        }

        public async Task<ExamResultDetailResponse> GetDetailByIdAsync(Guid id)
        {
            var response = await _dbContext.ExamResults
                                .AsNoTracking()
                                .Where(er => er.Id == id)
                                .Select(er => new ExamResultDetailResponse(
                                    er.Id,
                                    er.CompleteAt,
                                    er.Score,
                                    _dbContext.AnswerHistories
                                        .Where(ua => ua.ExamResultId == er.Id)
                                        .Select(ua => new UserAnswerResponse(
                                            ua.QuestionText,
                                            ua.QuestionTypes,
                                            ua.UserAnswer,
                                            ua.IsCorrect,
                                            ua.Score,
                                            ua.Explanation,
                                            JsonSerializer.Deserialize<List<Option>>(ua.OptionsJson, serializerOptions)!
                                        )).ToList()
                                )).FirstOrDefaultAsync();
            return response;
        }

        public async Task UpsertAsync(ExamResultReadModel examResultReadModel)
        {
            var examResult = _mapper.Map<ExamResult>(examResultReadModel);
            var answerHistories = _mapper.Map<ICollection<AnswerHistory>>(examResultReadModel.AnswerHistories);
            await _dbContext.ExamResults.Where(er => er.Id == examResult.Id).ExecuteDeleteAsync();
            _dbContext.ExamResults.Add(examResult);
            _dbContext.AnswerHistories.AddRange(answerHistories);
            await _dbContext.SaveChangesAsync();
        }
    }
}
