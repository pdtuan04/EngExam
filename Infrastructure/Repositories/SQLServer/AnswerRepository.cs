using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Repositories;
using AutoMapper;
using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SQLServer
{
    public class AnswerRepository: GenericRepository<Domain.Entity.Answer, Answer, Guid>, IAnswerRepository
    {
        public AnswerRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
