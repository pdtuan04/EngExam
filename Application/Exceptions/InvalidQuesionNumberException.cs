using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class InvalidQuesionNumberException : BusinessException
    {
        public InvalidQuesionNumberException(string message = "Question number is invalid.") : base(message, 400) { }
    }
}
