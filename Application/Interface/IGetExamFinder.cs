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
        public Task<ExamResponse> GetExamByIdAsync(Guid id);
        //public Task<IEnumerable<ExamResponse>> GetExamByCategory(Guid id);
    }
}
