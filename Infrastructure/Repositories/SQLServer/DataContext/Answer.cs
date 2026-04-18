using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class Answer : BaseEntity, ISoftDeletable
    {
        public required string Content { get; set; }
        public required bool IsCorrect { get; set; } = false;
        public required Guid QuestionId { get; set; }
        public Question Question { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
    }
}
