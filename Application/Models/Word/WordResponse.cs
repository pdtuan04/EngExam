using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Word
{
    public sealed record WordResponse(Guid Id, string Text, IEnumerable<string> Meanings);

}
