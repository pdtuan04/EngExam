using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Requests.Exam;
using Application.Repositories;
using Domain.Entity;

namespace Application.Interface
{
    public interface ISubmitExam
    {
        Task<ExamResult> SubmitExamAsync(SubmitExamRequest submit, Guid userId);
    }
}
