using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Word
{
    public sealed record CreateWordRequest(string Text, string Meaning);
}
