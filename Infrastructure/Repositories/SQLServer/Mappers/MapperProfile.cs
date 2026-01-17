using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Infrastructure.Repositories.SQLServer.Mappers
{
    public class MapperProfile:Profile
    {
        public MapperProfile() 
        {
            CreateMap<DataContext.Question,Domain.Entity.Question>();
            CreateMap<DataContext.Answer,Domain.Entity.Answer>();
            CreateMap<Domain.Entity.Answer, DataContext.Answer>();
            CreateMap<Domain.Entity.Question, DataContext.Question>();
            CreateMap<Domain.Entity.Exam, DataContext.Exam>();
            CreateMap<DataContext.Exam, Domain.Entity.Exam>();
            CreateMap<DataContext.ExamDetail, Domain.Entity.ExamDetail>();
            CreateMap<Domain.Entity.ExamDetail, DataContext.ExamDetail>();
            CreateMap<DataContext.ExamResult, Domain.Entity.ExamResult>();
            CreateMap<Domain.Entity.ExamResult, DataContext.ExamResult>();
            CreateMap<DataContext.AnswersHistory, Domain.Entity.AnswerHistory>();
            CreateMap<Domain.Entity.AnswerHistory, DataContext.AnswersHistory>();
            CreateMap<DataContext.User, Domain.Entity.User>();
            CreateMap<Domain.Entity.User, DataContext.User>();
            CreateMap<DataContext.ExamCategory, Domain.Entity.ExamCategory>();
            CreateMap<Domain.Entity.ExamCategory, DataContext.ExamCategory>();
        }
    }
}
