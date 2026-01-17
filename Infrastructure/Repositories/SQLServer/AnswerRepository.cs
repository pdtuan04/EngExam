using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using AutoMapper;
using Infrastructure.Repositories.SQLServer.DataContext;
using Infrastructure.Repositories.SQLServer.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SQLServer
{
    public class AnswerRepository: IAnswerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AnswerRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this._context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task AddAsync(Domain.Entity.Answer answer)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Domain.Entity.Answer>> GetAllAsync()
        {
            var answers = await _context.Answers.ToListAsync();
            return _mapper.Map<IEnumerable<Domain.Entity.Answer>>(answers);
        }

        public Task GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Domain.Entity.Answer>> GetAnswerByQuestionId(Guid idQuestion)
        {
            var answers = await _context.Answers
                .Where(a => a.QuestionId == idQuestion)
                .ToListAsync();
            return _mapper.Map<IEnumerable<Domain.Entity.Answer>>(answers);
        }
    }
}
