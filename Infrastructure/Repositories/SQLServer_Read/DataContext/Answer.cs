using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Infrastructure.Repositories.SQLServer_Read.DataContext
{
    public class Answer : BaseEntity, ISoftDeletable
    {
        public required string Content { get; set; }
        public required bool IsCorrect { get; set; } = false;
        public required Guid QuestionId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
