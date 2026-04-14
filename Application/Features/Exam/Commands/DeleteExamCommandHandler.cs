using Application.Abstractions;
using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Exam.Commands
{
    public sealed class DeleteExamCommandHandler : ICommandHandler<DeleteExamCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteExamCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ExamRepository.SoftDelete(request.Id);
            return true;
        }
    }
}
