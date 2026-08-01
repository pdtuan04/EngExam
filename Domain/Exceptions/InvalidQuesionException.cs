using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class InvalidQuestionException : DomainException
    {
        public InvalidQuestionException(string message) : base(message)
        {
        }
    }
}
