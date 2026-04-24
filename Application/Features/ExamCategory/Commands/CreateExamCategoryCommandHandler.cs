using Application.Abstractions;
using Application.Abstractions.Caching;
using Application.Abstractions.Events;
using Application.Abstractions.Messaging;
using Application.Features.ExamCategory.Events;
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
        private readonly IEventBus _eventBus;
        public CreateExamCategoryCommandHandler(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
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
            var examCategoryCreatedEvent = new CreateExamCategoryEvent(examCategory.Id, examCategory.Name, examCategory.Description, examCategory.ImageUrl, examCategory.IsActive);
            await _eventBus.PublishAsync(examCategoryCreatedEvent,cancellationToken);
            return new ExamCategoryResponse(examCategory.Id, examCategory.Name, examCategory.Description, examCategory.ImageUrl);
        }
    }
}
