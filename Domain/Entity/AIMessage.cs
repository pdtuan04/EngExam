using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class AIMessage
    {
        public required string Role { get; set; } = "User";
        public required string Content { get; set; }

    }
}
