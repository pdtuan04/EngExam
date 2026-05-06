using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read.DataContext
{
    public class PracticeDetail
    {
        public required Guid PracticeId { get; set; }
        public required Guid QuestionId { get; set; }
    }
}
