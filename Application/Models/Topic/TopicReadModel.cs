using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Topic
{
    public sealed record TopicReadModel(Guid Id, string Name, string Description, DateTime CreatedAt, DateTime UpdatedAt);
}
