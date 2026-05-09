using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using Application.Models.Course;
using Application.Models.Pagination;
using Application.Models.Topic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Common;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public class CourseReadRepository : ICourseReadRepository
    {
        private readonly ApplicationDbReadContext _dbContext;
        private readonly IMapper _mapper;

        public CourseReadRepository(ApplicationDbReadContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task DeleteAsync(Guid id, DateTime DeletedAt)
        {
            var course = await _dbContext.Courses
            .IgnoreQueryFilters()
            .AsTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
            if (course != null)
            {
                if (course.UpdatedAt >= DeletedAt)
                {
                    return;
                }
                course.IsDeleted = true;
                course.UpdatedAt = DeletedAt;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<CourseDetailResponse?> GetByIdAsync(Guid courseId, CancellationToken cancellationToken)
        {
            var course = await _dbContext.Courses.AsNoTracking().FirstOrDefaultAsync(c => c.Id == courseId, cancellationToken);
            return _mapper.Map<CourseDetailResponse>(course);
        }

        public async Task<PaginationResponse<CourseResponse>> GetPaginatedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _dbContext.Courses.AsNoTracking();
            var projectedQuery = query.ProjectTo<CourseResponse>(_mapper.ConfigurationProvider);
            var queryExecute = await PaginationDb<CourseResponse>.ToPagedList(projectedQuery, pageNumber, pageSize);
            return new PaginationResponse<CourseResponse>(queryExecute.Items, queryExecute.TotalCount, pageNumber, pageSize);
        }

        public async Task UpsertAsync(CourseReadModel course)
        {
            var existingCourse = await _dbContext.Courses.IgnoreQueryFilters().AsTracking().FirstOrDefaultAsync(t => t.Id == course.Id);
            if (existingCourse != null)
            {
                if (existingCourse.UpdatedAt >= course.UpdatedAt)
                {
                    return;
                }
                _mapper.Map(course, existingCourse);
            }
            else
            {
                var newCourse = _mapper.Map<Course>(course);
                newCourse.IsDeleted = false;
                _dbContext.Courses.Add(newCourse);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
