using Application.DTOs.Responses;
using Application.Interface;
using Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class GetExamCategory : IGetExamCategory
    {
        private readonly IExamCategoryRepository _examCategoryRepository;
        public GetExamCategory(IExamCategoryRepository examCategoryRepository)
        {
            _examCategoryRepository = examCategoryRepository ?? throw new ArgumentNullException(nameof(examCategoryRepository));
        }
        public async Task<IEnumerable<ExamCategoryDto>?> GetAllExamCategoryAsync()
        {
            var resutl = await _examCategoryRepository.GetAllAsync();
            if(resutl == null) return null;
            var examCategorys = resutl
                .Select(ec => new ExamCategoryDto
                {
                    Id = ec.Id,
                    Name = ec.Name,
                    Description = ec.Description,
                    ImageUrl = ec.ImageUrl,
                });
            return examCategorys;
        }
    }
}
