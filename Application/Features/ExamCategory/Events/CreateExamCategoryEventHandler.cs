using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamCategory.Events
{
    public class CreateExamCategoryEventHandler : INotificationHandler<CreateExamCategoryEvent>
    {
        public async Task Handle(CreateExamCategoryEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
