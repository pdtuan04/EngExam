using Application.Abstractions;
using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamCategory.Commands
{
    public sealed class DeleteExamCategoryCommandHandler : ICommandHandler<DeleteExamCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteExamCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<bool> Handle(DeleteExamCategoryCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ExamCategoryRepository.SoftDelete(request.Id);
            return true;
        }
    }
}
