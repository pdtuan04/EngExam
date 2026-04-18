using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Exam;
using Application.Models.ExamResult;
using Application.Models.Practice;
using Application.Models.Topic;
using AutoMapper;
using Infrastructure.Repositories.SQLServer_Read.DataContext;

namespace Infrastructure.Repositories.SQLServer_Read.Mappers
{
    public class MapperProfile:Profile
    {
        public MapperProfile() 
        {
            //Map between Domain and DataContext
            CreateMap<Question, Domain.Entity.Question>().ReverseMap();
            CreateMap<Answer, Domain.Entity.Answer>().ReverseMap();
            CreateMap<Exam, Domain.Entity.Exam>().ReverseMap();
            CreateMap<ExamDetail, Domain.Entity.ExamDetail>().ReverseMap();
            CreateMap<ExamResult, Domain.Entity.ExamResult>().ReverseMap();
            CreateMap<AnswersHistory, Domain.Entity.AnswerHistory>().ReverseMap();
            CreateMap<User, Domain.Entity.User>().ReverseMap();
            CreateMap<ExamCategory, Domain.Entity.ExamCategory>().ReverseMap();
            CreateMap<Practice, Domain.Entity.Practice>().ReverseMap();
            CreateMap<PracticeDetail, Domain.Entity.PracticeDetail>().ReverseMap();
            CreateMap<Course, Domain.Entity.Course>().ReverseMap();
            CreateMap<Topic, Domain.Entity.Topic>().ReverseMap();
            //Map between Infrastructure and Application Models for reading
            CreateMap<Topic, TopicResponse>().ReverseMap();
            CreateMap<Exam, ExamResponse>().ReverseMap();
            CreateMap<Practice, PracticeResponse>().ReverseMap();
            CreateMap<ExamResult, ExamResultResponse>().ReverseMap();
        }
    }
}
