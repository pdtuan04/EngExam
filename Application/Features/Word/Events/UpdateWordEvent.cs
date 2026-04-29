using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Word.Events
{
    public sealed record UpdateWordEvent(Guid Id, string Text, string Meaning);
}
