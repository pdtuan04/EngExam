using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories.Read;
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
        private readonly IExamCategoryReadRepository _examCategoryReadRepository;
        public GetExamCategoryByIdQueryHandler(IExamCategoryReadRepository examCategoryReadRepository)
        {
            _examCategoryReadRepository = examCategoryReadRepository;
        }
        public async Task<ExamCategoryResponse> Handle(GetExamCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _examCategoryReadRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Category", request.Id);
        }
    }
}
