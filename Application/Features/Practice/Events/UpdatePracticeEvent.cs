using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Practice.Events
{
    public sealed record UpdatePracticeEvent(Guid PracticeId, string Title, string Description, DateTime CreatedAt, DateTime UpdatedAt, Guid TopicId);
}
