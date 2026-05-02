using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class NotFoundException : BusinessException
    {
        public NotFoundException()
            : base("The requested resource was not found.",404)
        {
        }
        public NotFoundException(string name, object key)
            : base($"{name}({key}) was not found.",404)
        {
        }
    }
}
