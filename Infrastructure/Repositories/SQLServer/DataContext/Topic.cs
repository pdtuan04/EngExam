using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class Topic : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public ICollection<Question> Questions { get; set; } = [];
    }
}
