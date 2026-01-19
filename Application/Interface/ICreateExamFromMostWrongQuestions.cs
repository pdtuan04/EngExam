using Application.DTOs.Exam.Management;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ICreateExamFromMostWrongQuestions
    {
        public Task<Exam> CreateExam(CreateExamDTO exam);
    }
}
