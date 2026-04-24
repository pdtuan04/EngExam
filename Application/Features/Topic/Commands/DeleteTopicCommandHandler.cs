using Application.Abstractions;
using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Topic.Commands
{
    public sealed class DeleteTopicCommandHandler : ICommandHandler<DeleteTopicCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteTopicCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.TopicRepository.Delete(request.Id);
            return result;
        }
    }
}
