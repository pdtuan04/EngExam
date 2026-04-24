using Application.Abstractions;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Features.Exam.Events;
using Application.Features.Practice.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Practice.Commands
{
    public sealed class DeletePracticeCommandHandler : ICommandHandler<DeletePracticeCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;   
        public DeletePracticeCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }
        public async Task<bool> Handle(DeletePracticeCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.PracticeRepository.Delete(request.Id);
            await _eventBus.PublishAsync(new DeletePracticeEvent(request.Id));
            return result;
        }
    }
}
