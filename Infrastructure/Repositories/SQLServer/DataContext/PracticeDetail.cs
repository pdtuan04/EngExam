using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class PracticeDetail
    {
        public required Guid PracticeId { get; set; }
        public Practice Practice { get; set; } = null!;
        public required Guid QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }
}
