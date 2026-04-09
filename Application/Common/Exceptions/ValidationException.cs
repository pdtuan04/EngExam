using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation errors occurred.")
        {
        }

        public ValidationException(string message)
            : base(message)
        {
        }
    }
}
