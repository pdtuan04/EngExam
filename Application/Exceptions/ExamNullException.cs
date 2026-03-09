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
            : base("Exam not found.") { }

        public ExamNullException(string message)
            : base(message) { }
    }
}
