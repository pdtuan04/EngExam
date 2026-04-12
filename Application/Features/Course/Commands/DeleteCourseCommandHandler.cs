using Application.Abstractions.Messaging;
using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Course.Commands
{
    public class DeleteCourseCommandHandler : ICommandHandler<DeleteCourseCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        DeleteCourseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException();
        }
        public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.CourseRepository.Delete(request.Id);
        }
    }
}
