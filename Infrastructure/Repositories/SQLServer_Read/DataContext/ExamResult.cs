using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Infrastructure.Repositories.SQLServer_Read.DataContext;

namespace Infrastructure.Repositories.SQLServer_Read.DataContext
{
    public class ExamResult
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required int DurationInMinutes { get; set; }
        public required DateTime CompleteAt { get; set; }
        public required double Score { get; set; } = 0;
        public required Guid ExamId { get; set; }
        public required Guid UserId { get; set; }
    }
}
