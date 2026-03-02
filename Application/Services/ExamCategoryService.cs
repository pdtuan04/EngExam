using Application.Common.Interfaces;
using Application.Models.ExamCategory;
using Application.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ExamCategoryService : IExamCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExamCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ICollection<ExamCategoryResponse>> GetAll()
        {
            var result = await _unitOfWork.ExamCategoryRepository.GetAllAsync();
            return result.Select(x => new ExamCategoryResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl
            }).ToList();
        }

        public async Task<PaginationResponse<ExamCategoryResponse>> GetPaginated(PaginatedRequest request)
        {
            return await _unitOfWork.ExamCategoryRepository.ToPagination(
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                orderBy: x => x.Name,
                ascending: true,
                selector: x => new ExamCategoryResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl
                }
            );
        }
    }
}
