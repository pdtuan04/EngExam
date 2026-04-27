using Application.Abstractions.Messaging;
using Application.Models.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Word.Commands
{
    public sealed record CreateWordCommand(string Text, IEnumerable<string> Meanings) : ICommand<WordResponse>;
}
