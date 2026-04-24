using Application.Abstractions.Messaging;
using Application.Models.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Topic.Commands
{
    public sealed record CreateTopicCommand(string Name, string Description) : ICommand<TopicResponse>;
}
