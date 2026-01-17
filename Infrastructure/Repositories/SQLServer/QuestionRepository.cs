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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public QuestionRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this._context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task AddAsync(Domain.Entity.Question question)
        {
            var dbquestion = _mapper.Map<Question>(question);
            _context.Questions.Add(dbquestion);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var dbQuestion = await _context.Questions.FindAsync(id);
            if (dbQuestion == null)
            {
                return false;
            }
            _context.Questions.Remove(dbQuestion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Domain.Entity.Question>> GetAllAsync()
        {
            var questions = await _context.Questions.ToListAsync();
            return _mapper.Map<IEnumerable<Domain.Entity.Question>>(questions);
        }

        public async Task<Domain.Entity.Question> GetByIdAsync(Guid id)
        {
            var question = await _context.Questions.FindAsync(id);
            return _mapper.Map<Domain.Entity.Question>(question);
        }

        public async Task UpdateAsync(Domain.Entity.Question question)
        {
            var dbQuestion = await _context.Questions.FindAsync(question.Id);
            _mapper.Map(question, dbQuestion);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Domain.Entity.Question>> GetByIdExamAsync(Guid id)
        {
            var dbQuestions = await _context.Questions
                .Include(q => q.Answers)
                .Where(q => q.ExamDetail.Any(ed => ed.ExamId == id))
                .ToListAsync();
            return _mapper.Map<IEnumerable<Domain.Entity.Question>>(dbQuestions);
        }
    }
}
