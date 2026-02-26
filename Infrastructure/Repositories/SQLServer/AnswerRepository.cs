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
    public class AnswerRepository: GenericRepository<Domain.Entity.Answer, Answer>, IAnswerRepository
    {
        public AnswerRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
