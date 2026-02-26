using Application.Repositories;
using AutoMapper;
using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer
{
    public class PracticeRepository : GenericRepository<Domain.Entity.Practice, Practice>, IPracticeRepository
    {
        public PracticeRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper) 
        {
        }
    }
}
