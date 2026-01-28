using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Exam.Doing;
using Domain.Entity;

namespace Application.Interface.Exam
{
    public interface IGetRandomExam
    {
        public Task<ExamForDoingDTO> GetRandomExamAsync();
    }
}
