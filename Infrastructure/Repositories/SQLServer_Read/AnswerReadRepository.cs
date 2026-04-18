using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Repositories;
using Application.Abstractions.Repositories.Read;
using AutoMapper;
using Infrastructure.Repositories.SQLServer.Mappers;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SQLServer_Read
{
    public class AnswerReadRepository: GenericReadRepository<Domain.Entity.Answer, Answer>, IAnswerReadRepository
    {
        public AnswerReadRepository(ApplicationDbReadContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
