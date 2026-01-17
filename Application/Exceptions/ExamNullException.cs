using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ExamNullException:Exception
    {
        public ExamNullException()
            : base("Exam object is null.") { }

        public ExamNullException(string message)
            : base(message) { }
    }
}
