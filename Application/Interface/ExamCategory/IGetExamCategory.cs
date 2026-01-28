using Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.ExamCategory
{
    public interface IGetExamCategory
    {
        public Task<IEnumerable<ExamCategoryDto>?> GetAllExamCategoryAsync();
    }
}
