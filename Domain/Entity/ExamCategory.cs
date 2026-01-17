using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class ExamCategory:BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public ICollection<Exam> Exams { get; set; } = [];
    }
}
