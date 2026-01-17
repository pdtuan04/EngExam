using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class DetailResultByQuestionTypesHandler : Exception
    {
        public DetailResultByQuestionTypesHandler()
        {
        }
        public DetailResultByQuestionTypesHandler(string message) : base(message)
        {
        }
        public DetailResultByQuestionTypesHandler(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
