using Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IGetExamCategory
    {
        public Task<ExamCategoryResponse> GetAllExamCategoryAsync();
    }
}
