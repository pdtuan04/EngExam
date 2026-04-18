using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SQLServer_Read.DataContext
{
    public class User : IdentityUser<Guid>
    {
        public int? Age { get; set; }
        public ICollection<ExamResult> ExamResults { get; set;} = null!;
    }
}
