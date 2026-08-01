using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public sealed class InvalidTotalScoreException : BusinessException
    {
        public InvalidTotalScoreException(double score)
        : base($"The total score must be 100 points. Current total score: {score}.", 400)
        {
        }
    }
}
