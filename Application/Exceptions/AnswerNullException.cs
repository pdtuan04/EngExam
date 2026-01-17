using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class AnswerNullException:Exception
    {
        public AnswerNullException()
            : base("Answer object is null.") { }
        public AnswerNullException(string message)
            : base(message) { }
    }
}
