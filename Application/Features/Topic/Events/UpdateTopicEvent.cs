using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Topic.Events
{
    public sealed record UpdateTopicEvent(Guid Id, string Name, string Description, DateTime CreatedAt, DateTime UpdatedAt);
}
