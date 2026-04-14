using Application.Abstractions.Messaging;
using Application.Models.ExamCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ExamCategory.Commands
{
    public sealed record CreateExamCategoryCommand(string Name,string Description, string? ImageUrl = null) : ICommand<ExamCategoryResponse>;
}
