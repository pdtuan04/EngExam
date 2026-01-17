using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Responses;
using Domain.Entity;

namespace Application.Interface
{
    public interface IGetRandomExam
    {
        public Task<ExamForDoingDto> GetRandomExamAsync();
    }
}
