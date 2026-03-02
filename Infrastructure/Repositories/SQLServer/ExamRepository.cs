using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Repositories;
using AutoMapper;
using Domain.Entity;
using Infrastructure.Common;
using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SQLServer
{
    public class ExamRepository : GenericRepository<Domain.Entity.Exam, DataContext.Exam>, IExamRepository
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
                .FirstOrDefaultAsync(e => e.IsActicve == true);
            return _mapper.Map<Domain.Entity.Exam>(randomExam);
        }
        public async Task<IEnumerable<Domain.Entity.Exam>> GetExamsByCategoryIdAsync(Guid categoryId)
        {
            var dbExams = await _dbContext.Exams
                .Where(e => e.ExamCategoryId == categoryId && e.IsActicve == true)
                .ToListAsync();
            return _mapper.Map<IEnumerable<Domain.Entity.Exam>>(dbExams);
        }

        public async Task<Guid> AddAsync(Domain.Entity.Exam exam)
        {
            var dbExam = _mapper.Map<DataContext.Exam>(exam);
            await _dbContext.Exams.AddAsync(dbExam);
            return dbExam.Id;
        }

        public async Task<Domain.Entity.Exam> GetExamToTake(Guid id)
        {
            var dbExam = await _dbContext.Exams
                .AsNoTracking()
                .Include(e => e.ExamDetail)
                .ThenInclude(ed => ed.Question)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActicve == true);
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
            dbExam.IsActicve = false;
            return true;
        }
    }
}
