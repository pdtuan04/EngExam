using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repositories;
using AutoMapper;
using Domain.Entity;
using Infrastructure.Common;
using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SQLServer
{
    public class ExamRepository : IExamRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ExamRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Domain.Entity.Exam>> GetAllAsync()
        {
            var dbExams = await _context.Exams.ToListAsync();
            return _mapper.Map<IEnumerable<Domain.Entity.Exam>>(dbExams);
        }

        public async Task<Domain.Entity.Exam> GetRandomExam()
        {
            var randomExam = await _context.Exams
                .Include(e => e.ExamDetail)
                .ThenInclude(ed => ed.Question)
                .ThenInclude(q => q.Answers)
                .OrderBy(x => Guid.NewGuid())
                .FirstOrDefaultAsync();
            return _mapper.Map<Domain.Entity.Exam>(randomExam);
        }
        public async Task<Domain.Entity.Exam> GetByIdAsync(Guid id)
        {
            var dbExam = await _context.Exams
                .Include(ed => ed.ExamDetail)
                .ThenInclude(q => q.Question)
                .ThenInclude(a => a.Answers)
                .FirstOrDefaultAsync(ed => ed.Id == id);
            var exam = _mapper.Map<Domain.Entity.Exam>(dbExam);
            return _mapper.Map<Domain.Entity.Exam>(dbExam);
        }
        public async Task<IEnumerable<Domain.Entity.Exam>> GetExamsByCategoryIdAsync(Guid categoryId)
        {
            var dbExams = await _context.Exams
                .Where(e => e.ExamCategoryId == categoryId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<Domain.Entity.Exam>>(dbExams);
        }

        public async Task<Guid> AddAsync(Domain.Entity.Exam exam)
        {
            var dbExam = _mapper.Map<DataContext.Exam>(exam);
            await _context.Exams.AddAsync(dbExam);
            return dbExam.Id;
        }

        public async Task<Application.DTOs.PaginatedList<Domain.Entity.Exam>> GetPaginatedExamAsync(string? search, string? sortBy, string sortDir, int pageNumber, int pageSize)
        {
            IQueryable<DataContext.Exam> query = _context.Exams;
            //search theo title mai mot search cai khac tinh sau :()
            if (!string.IsNullOrEmpty(search))
                query = query.Where(e => e.Title.Contains(search));
            if (!string.IsNullOrEmpty(sortBy))
            {
                var entitiesProperty = typeof(DataContext.Exam)
                    .GetProperty(sortBy ?? throw new Exception("System doesn't have any this property"));
                query = sortDir == "desc"
                    ? query.OrderByDescending(e => EF.Property<object>(e, entitiesProperty.Name))
                    : query.OrderBy(e => EF.Property<object>(e, entitiesProperty.Name));
            }
            else
            {
                query = query.OrderBy(e => e.Title);
            }
            var paginated = await PaginatedList<DataContext.Exam>.CreatePageAsync(query, pageNumber, pageSize);
            var mappedItems = _mapper.Map<IEnumerable<Domain.Entity.Exam>>(paginated.Items);

            return await Application.DTOs.PaginatedList<Domain.Entity.Exam>.CreatePageAsync(mappedItems, paginated.PageNumber, paginated.PageSize, paginated.TotalCount);
        }
    }
}
