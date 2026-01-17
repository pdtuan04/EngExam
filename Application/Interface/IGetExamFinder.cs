using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Responses;

namespace Application.Interface
{
    public interface IGetExamFinder
    {
        public Task<ExamForDoingDto> GetExamForDoingAsync(Guid id);
        public Task<IEnumerable<ExamSummaryDto>?> GetExamsByCategoryIdAsync(Guid id);
    }
}
