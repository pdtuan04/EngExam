using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class VocabularyNotFoundException : DomainException
    {
        public VocabularyNotFoundException(string message) : base(message)
        {
        }
    }
}
