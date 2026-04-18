using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using AutoMapper;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public class ExamReadRepository : GenericReadRepository<Domain.Entity.Exam, Exam>, IExamReadRepository
    {
        public ExamReadRepository(ApplicationDbReadContext context, IMapper mapper) : base(context, mapper)
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
    }
}
