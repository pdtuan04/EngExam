using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Models.ExamCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamCategory.Commands
{
    public sealed class CreateExamCategoryCommandHandler : ICommandHandler<CreateExamCategoryCommand, ExamCategoryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateExamCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ExamCategoryResponse> Handle(CreateExamCategoryCommand request, CancellationToken cancellationToken)
        {
            var examCategory = new Domain.Entity.ExamCategory
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                IsActive = true
            };
            await _unitOfWork.ExamCategoryRepository.AddAsync(examCategory);
            return new ExamCategoryResponse(examCategory.Id, examCategory.Name, examCategory.Description, examCategory.ImageUrl);
        }
    }
}
