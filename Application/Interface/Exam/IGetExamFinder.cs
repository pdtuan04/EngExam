using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Exam.Doing;
using Application.DTOs.Responses;

namespace Application.Interface.Exam
{
    public interface IGetExamFinder
    {
        public Task<ExamForDoingDTO> GetExamForDoingAsync(Guid id);
        public Task<IEnumerable<ExamSummaryDto>?> GetExamsByCategoryIdAsync(Guid id);
    }
}
