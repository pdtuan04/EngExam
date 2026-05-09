using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Models.Question;
using AutoMapper;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public class QuestionReadRepository : IQuestionReadRepository
    {
        private readonly ApplicationDbReadContext _dbContext;
        private readonly IMapper _mapper;

        public QuestionReadRepository(ApplicationDbReadContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task DeleteAsync(Guid id, DateTime deletedAt)
        {
            var question = await _dbContext.Questions
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(t => t.Id == id);
            if (question != null)
            {
                if (question.UpdatedAt >= deletedAt)
                {
                    return;
                }
                question.IsDeleted = true;
                question.UpdatedAt = deletedAt;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBulkAsync(IEnumerable<Guid> ids, DateTime deletedAt)
        {
            var questions = await _dbContext.Questions
                .IgnoreQueryFilters()
                .Where(t => ids.Contains(t.Id) && t.UpdatedAt < deletedAt)
                .ExecuteUpdateAsync(t => t
                    .SetProperty(q => q.IsDeleted, true)
                    .SetProperty(q => q.UpdatedAt, deletedAt));
        }

        public async Task UpsertAsync(QuestionReadModel question)
        {
            var existingQuestion = await _dbContext.Questions.IgnoreQueryFilters().FirstOrDefaultAsync(t => t.Id == question.Id);
            if (existingQuestion != null)
            {
                if (existingQuestion.UpdatedAt >= question.UpdatedAt)
                {
                    return;
                }
                _mapper.Map(question, existingQuestion);
            }
            else
            {
                var newQuestion = _mapper.Map<Question>(question);
                newQuestion.IsDeleted = false;
                _dbContext.Questions.Add(newQuestion);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpsertBulkAsync(IEnumerable<QuestionReadModel> questions)
        {
            var questionIds = questions.Select(q => q.Id).ToList();
            var existingQuestions = await _dbContext.Questions
                .IgnoreQueryFilters()
                .Where(t => questionIds.Contains(t.Id))
                .ToDictionaryAsync(q => q.Id, q => q);
            var newQuestion = new List<Question>();
            foreach (var question in questions)
            {
                if(existingQuestions.TryGetValue(question.Id, out var existingQuestion))
                {
                    if(existingQuestion.UpdatedAt < question.UpdatedAt)
                    {
                        _mapper.Map(question, existingQuestion);
                    }
                }
                else
                {
                    var newQuestionEntity = _mapper.Map<Question>(question);
                    newQuestionEntity.IsDeleted = false;
                    newQuestion.Add(newQuestionEntity);
                }
            }
            if (newQuestion.Any())
            {
                await _dbContext.Questions.AddRangeAsync(newQuestion);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
