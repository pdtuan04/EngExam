using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.Common.Exceptions;
using Application.Models.ExamCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamCategory.Commands
{
    public sealed class UpdateExamCategoryCommandHandler : ICommandHandler<UpdateExamCategoryCommand, ExamCategoryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateExamCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<ExamCategoryResponse> Handle(UpdateExamCategoryCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var examCategory = await _unitOfWork.ExamCategoryRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Category", request.Id);
            examCategory.Name = request.Name;
            examCategory.Description = request.Description;
            examCategory.ImageUrl = request.ImageUrl;
            examCategory.UpdatedAt = now;
            await _unitOfWork.ExamCategoryRepository.Update(examCategory);
            return new ExamCategoryResponse(examCategory.Id, examCategory.Name, examCategory.Description, examCategory.ImageUrl);
        }
    }
}
