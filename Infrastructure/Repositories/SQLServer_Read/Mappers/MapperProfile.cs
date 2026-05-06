using Application.Models.Course;
using Application.Models.Exam;
using Application.Models.ExamResult;
using Application.Models.FlashCard;
using Application.Models.Practice;
using Application.Models.Topic;
using Application.Models.Word;
using AutoMapper;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
            CreateMap<Word, Domain.Entity.Word>().ReverseMap();
            CreateMap<FlashCard, Domain.Entity.FlashCard>().ReverseMap();
            CreateMap<TopicReadModel, Topic>().ReverseMap();
            //Map between Infrastructure and Application Models for reading

            //Exam result 
            CreateMap<ExamResult, ExamResultDetailResponse>()
                .ForMember(dest => dest.TotalScore, otp => otp.MapFrom(src => src.Score));
            CreateMap<AnswersHistory, UserAnswerResponse>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.QuestionText))
                .ForMember(dest => dest.EarnedPoint, opt => opt.MapFrom(src => src.Score))
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src =>
                    string.IsNullOrEmpty(src.OptionsJson)
                        ? new List<Option>()
                        : JsonSerializer.Deserialize<List<Option>>(src.OptionsJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ));
            CreateMap<ExamResultReadModel, ExamResult>();
            CreateMap<AnswerHistoryReadModel, AnswersHistory>();

            CreateMap<Topic, TopicResponse>().ReverseMap();
            CreateMap<Exam, ExamResponse>().ReverseMap();
            CreateMap<Practice, PracticeResponse>().ReverseMap();
            CreateMap<ExamResult, ExamResultResponse>().ReverseMap();
            CreateMap<Word, WordResponse>().ReverseMap();
            CreateMap<FlashCard, FlashCardResponse>().ReverseMap();
            CreateMap<FlashCard, FlashCardDetailResponse>().ReverseMap();
            CreateMap<Course, CourseDetailResponse>().ReverseMap();
            CreateMap<Course, CourseResponse>().ReverseMap();
        }
    }
}
