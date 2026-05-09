using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Models.Answer;
using AutoMapper;
using Infrastructure.Repositories.SQLServer.Mappers;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public class AnswerReadRepository: IAnswerReadRepository
    {
        private readonly ApplicationDbReadContext _dbContext;
        private readonly IMapper _mapper;

        public AnswerReadRepository(ApplicationDbReadContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task UpsertBulkAsync(IEnumerable<AnswerReadModel> answers)
        {
            var newAnswerEntities = new List<Answer>();
            var answerIds = answers.Select(a => a.Id).ToList();
            var existingAnswers = await _dbContext.Answers
                                                .IgnoreQueryFilters()
                                                .AsTracking()
                                                .Where(a => answerIds.Contains(a.Id))
                                                .ToDictionaryAsync(a => a.Id, a => a);
            foreach(var answer in answers)
            {
                if(existingAnswers.TryGetValue(answer.Id, out var existingAnswer))
                {
                    if(existingAnswer.UpdatedAt < answer.UpdatedAt)
                    {
                        _mapper.Map(answer, existingAnswer);
                    }
                }
                else
                {
                    var newAnswer = _mapper.Map<Answer>(answer);
                    newAnswerEntities.Add(newAnswer);
                }
            }
            if (newAnswerEntities.Any())
            {
                await _dbContext.Answers.AddRangeAsync(newAnswerEntities);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
