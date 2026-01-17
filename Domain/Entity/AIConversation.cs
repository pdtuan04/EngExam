using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class AIConversation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ICollection<AIMessage> Messages { get; set; } = [];
    }
}
