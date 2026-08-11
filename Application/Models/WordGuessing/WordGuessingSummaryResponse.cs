using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.WordGuessing
{
    public sealed record WordGuessingSummaryResponse(string Player1Name, string Player2Name, int Player1Score, int Player2Score, string Message);
}
