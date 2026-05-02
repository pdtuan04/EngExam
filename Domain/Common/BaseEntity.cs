using Domain.Abstractions.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class BaseEntity<TKey> : IEntity<TKey>
    {
        public required TKey Id { get; set; } 
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
