using Application.Abstractions.Messaging;
using Application.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Course.Commands
{
    public sealed record UpdateCourseCommand(Guid Id, string Name, string Description, string Content, string ImageUrl, Guid TopicId) : ICommand<CourseResponse>;
}
