using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Abstractions.Repositories;
using Application.Features.ExamCategory.Events;
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
        private readonly IEventBus _eventBus;
        public DeleteExamCategoryCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }
        public async Task<bool> Handle(DeleteExamCategoryCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            await _unitOfWork.ExamCategoryRepository.SoftDelete(request.Id, now);
            await _eventBus.PublishAsync(new DeletedExamCategoryEvent(request.Id, now));
            return true;
        }
    }
}
