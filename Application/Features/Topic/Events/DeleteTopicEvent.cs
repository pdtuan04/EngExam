using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Topic.Events
{
    public sealed record DeleteTopicEvent(Guid Id);
}
