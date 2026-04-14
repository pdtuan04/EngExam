using Application.Abstractions.Messaging;
using Application.Models.ExamCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamCategory.Commands
{
    public sealed record UpdateExamCategoryCommand(Guid Id, string Name, string Description, string? ImageUrl) : ICommand<ExamCategoryResponse>;
}
