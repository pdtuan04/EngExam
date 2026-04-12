using Application.Repositories;
using AutoMapper;
using Infrastructure.Repositories.SQLServer.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer
{
    public class CourseRepository : GenericRepository<Domain.Entity.Course, Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
