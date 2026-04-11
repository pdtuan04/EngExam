using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public required Guid Id { get; set; } 
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; set; }
        public required bool IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;
        private void AddCreateAt()
        {
            CreatedAt = DateTime.UtcNow;
        }
        private void AddUpdateAt()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
