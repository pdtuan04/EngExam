using Application.DTOs.Responses;
using Application.Interface;
using Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class GetExamcategory : IGetExamCategory
    {
        private readonly IExamCategoryRepository _examCategoryRepository;
        public GetExamcategory(IExamCategoryRepository examCategoryRepository)
        {
            _examCategoryRepository = examCategoryRepository ?? throw new ArgumentNullException(nameof(examCategoryRepository));
        }
        public async Task<ExamCategoryResponse> GetAllExamCategoryAsync()
        {
            throw new NotImplementedException();
        }
    }
}
