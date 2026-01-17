using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class QuestionNullException:Exception
    {
        public QuestionNullException()
            : base("Question object is null.") { }
        public QuestionNullException(string message)
            : base(message) { }
    }
}
