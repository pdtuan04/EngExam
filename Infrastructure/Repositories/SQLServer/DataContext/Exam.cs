using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class Exam:BaseEntity
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required int DurationInMinutes { get; set; }
        public ICollection<ExamDetail> ExamDetail { get; set; } = [];
        public required Guid ExamCategoryId { get; set; }
        public ExamCategory? ExamCategory { get; set; }
    }
}
