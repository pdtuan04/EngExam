using Application.Abstractions.Messaging;
using Application.Models.FlashCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FlashCard.Commands
{
    public record UpdateFlashCardCommand(Guid Id, string Title, string? Description) : ICommand<FlashCardResponse>;
}
