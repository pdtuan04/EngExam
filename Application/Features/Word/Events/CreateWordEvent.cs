using Application.Abstractions.Messaging;
using Application.Models.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Word.Events
{
    public sealed record CreateWordEvent(Guid Id, string Text, string Meaning, Guid FlashCardId);
}
