using AutoMapper;
using Domain.Entity;
using EngExam.Models;
using Microsoft.AspNetCore.Http.HttpResults;
//D:\Workplace\Visual_Studio\ASP.NET\EngExam\EngExam\Mappers\Mapper2EntityProfile.cs
//đây là tầng presentation,dto sang domain.entity
namespace EngExam.Mappers
{
    public class Mapper2EntityProfile:Profile
    {
        public Mapper2EntityProfile() 
        {
            CreateMap<QuestionModel,Question>();
        }
    }
}
