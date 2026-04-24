using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Features.Exam.Events;
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
        private readonly IEventBus _eventBus;   
        public DeleteExamCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }
        public async Task<bool> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ExamRepository.SoftDelete(request.Id);
            await _eventBus.PublishAsync(new DeletedExamEvent(request.Id));
            return result;
        }
    }
}
