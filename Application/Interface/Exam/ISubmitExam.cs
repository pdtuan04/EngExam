using Application.DTOs.Requests.Exam;
using Application.DTOs.Responses;
using Application.Repositories;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Exam
{
    public interface ISubmitExam
    {
        Task<ExamResultDto> SubmitExamAsync(SubmitExamRequest submit, Guid userId);
    }
}
