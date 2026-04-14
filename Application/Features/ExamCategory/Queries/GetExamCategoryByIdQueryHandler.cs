using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Common.Exceptions;
using Application.Models.ExamCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamCategory.Queries
{
    internal class GetExamCategoryByIdQueryHandler : IQueryHandler<GetExamCategoryByIdQuery, ExamCategoryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetExamCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ExamCategoryResponse> Handle(GetExamCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var examCategory = await _unitOfWork.ExamCategoryRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Category", request.Id);
            return new ExamCategoryResponse(examCategory.Id, examCategory.Name, examCategory.Description, examCategory.ImageUrl);
        }
    }
}
