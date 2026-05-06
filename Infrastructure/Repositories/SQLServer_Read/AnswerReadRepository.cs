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
    public class AnswerReadRepository: IAnswerReadRepository
    {
        private readonly ApplicationDbReadContext _dbContext;
        private readonly IMapper _mapper;

        public AnswerReadRepository(ApplicationDbReadContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
    }
}
