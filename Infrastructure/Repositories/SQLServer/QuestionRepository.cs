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
    public class QuestionRepository : GenericRepository<Domain.Entity.Question, Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task AddAsync(Domain.Entity.Question question)
        {
            var dbquestion = _mapper.Map<Question>(question);
            _dbContext.Questions.Add(dbquestion);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var dbQuestion = await _dbContext.Questions.FindAsync(id);
            if (dbQuestion == null)
            {
                return false;
            }
            _dbContext.Questions.Remove(dbQuestion);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Domain.Entity.Question>> GetAllAsync()
        {
            var questions = await _dbContext.Questions.ToListAsync();
            return _mapper.Map<IEnumerable<Domain.Entity.Question>>(questions);
        }

        public async Task<Domain.Entity.Question> GetByIdAsync(Guid id)
        {
            var question = await _dbContext.Questions.FindAsync(id);
            return _mapper.Map<Domain.Entity.Question>(question);
        }

        public async Task UpdateAsync(Domain.Entity.Question question)
        {
            var dbQuestion = await _dbContext.Questions.FindAsync(question.Id);
            _mapper.Map(question, dbQuestion);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Domain.Entity.Question>> GetByIdExamAsync(Guid id)
        {
            var dbQuestions = await _dbContext.Questions
                .Include(q => q.Answers)
                .Where(q => q.ExamDetail.Any(ed => ed.ExamId == id))
                .ToListAsync();
            return _mapper.Map<IEnumerable<Domain.Entity.Question>>(dbQuestions);
        }
    }
}
