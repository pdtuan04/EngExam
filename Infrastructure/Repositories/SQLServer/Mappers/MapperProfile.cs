using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Repositories.SQLServer.DataContext;

namespace Infrastructure.Repositories.SQLServer.Mappers
{
    public class MapperProfile:Profile
    {
        public MapperProfile() 
        {
            CreateMap<Question, Domain.Entity.Question>().ReverseMap();
            CreateMap<Answer, Domain.Entity.Answer>().ReverseMap();
            CreateMap<Domain.Entity.Answer, Answer>().ReverseMap();
            CreateMap<Domain.Entity.Question, Question>().ReverseMap();
            CreateMap<Domain.Entity.Exam, Exam>().ReverseMap();
            CreateMap<Exam, Domain.Entity.Exam>().ReverseMap();
            CreateMap<ExamDetail, Domain.Entity.ExamDetail>().ReverseMap();
            CreateMap<Domain.Entity.ExamDetail, ExamDetail>().ReverseMap();
            CreateMap<ExamResult, Domain.Entity.ExamResult>().ReverseMap();
            CreateMap<Domain.Entity.ExamResult, ExamResult>().ReverseMap();
            CreateMap<AnswersHistory, Domain.Entity.AnswerHistory>().ReverseMap();
            CreateMap<Domain.Entity.AnswerHistory, AnswersHistory>().ReverseMap();
            CreateMap<User, Domain.Entity.User>().ReverseMap();
            CreateMap<Domain.Entity.User, User>().ReverseMap();

            CreateMap<ExamCategory, Domain.Entity.ExamCategory>().ReverseMap();
            CreateMap<Domain.Entity.ExamCategory, ExamCategory>().ReverseMap();

            CreateMap<Practice, Domain.Entity.Practice>().ReverseMap();

            CreateMap<PracticeDetail, Domain.Entity.PracticeDetail>().ReverseMap();
            CreateMap<Domain.Entity.PracticeDetail, PracticeDetail>().ReverseMap();
        }
    }
}
