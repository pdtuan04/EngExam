using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Models.ExamCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamCategory.Queries
{
    public sealed class GetAllCategoryQueryHandler : IQueryHandler<GetAllCategoryQuery, List<ExamCategoryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllCategoryQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<ExamCategoryResponse>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ExamCategoryRepository.GetAllAsync();
            return result.Select(x => new ExamCategoryResponse(x.Id, x.Name, x.Description, x.ImageUrl)).ToList();
        }
    }
}
