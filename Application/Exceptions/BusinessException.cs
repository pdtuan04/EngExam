using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class BusinessException : Exception
    {
        public int StatusCode { get;}
        public BusinessException(string message, int statusCode):base(message) 
        {
            StatusCode = statusCode;
        }
    }
}
