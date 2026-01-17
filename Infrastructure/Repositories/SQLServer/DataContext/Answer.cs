using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class Answer : BaseEntity
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; } = false;
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
