using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Models.ExamCategory;
using Application.Models.Pagination;
using Domain.Entity;
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
        public async Task<ExamCategoryResponse> CreateExamCategogy(CreateExamCategoryRequest request)
        {
            var examCategory = new ExamCategory
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                ImageUrl= request.ImageUrl,
                IsActive = true
            };
            await _unitOfWork.ExamCategoryRepository.AddAsync(examCategory);
            await _unitOfWork.SaveChangesAsync();
            return new ExamCategoryResponse
            {
                Id = examCategory.Id,
                Name = examCategory.Name,
                Description = examCategory.Description,
                ImageUrl = examCategory.ImageUrl,
            };
        }

        public async Task DeleteExamCategogy(Guid id)
        {
            await _unitOfWork.ExamCategoryRepository.SoftDelete(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ExamCategoryResponse> UpdateExamCategory(UpdateExamCategoryRequest request)
        {
            var now = DateTime.UtcNow;
            var examCategory = await _unitOfWork.ExamCategoryRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Category", request.Id);
            examCategory.Name = request.Name;
            examCategory.Description = request.Description;
            examCategory.ImageUrl = request.ImageUrl;
            examCategory.UpdatedAt = now;
            await _unitOfWork.ExamCategoryRepository.Update(examCategory);
            await _unitOfWork.SaveChangesAsync();
            return new ExamCategoryResponse
            {
                Id = examCategory.Id,
                Name = examCategory.Name,
                Description = examCategory.Description,
                ImageUrl = examCategory.ImageUrl,
            };
        }
        public async Task<ExamCategoryResponse> GetById(Guid id)
        {
            var examCategory = await _unitOfWork.ExamCategoryRepository.GetByIdAsync(id) ?? throw new NotFoundException("Category", id);
            return new ExamCategoryResponse
            {
                Id = examCategory.Id,
                Name = examCategory.Name,
                Description = examCategory.Description,
                ImageUrl = examCategory.ImageUrl,
            };
        }
    }
}
