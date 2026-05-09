using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstractions.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.SQLServer.DataContext
{
    public class User : IdentityUser<Guid> , IEntity<Guid>
    {
        public int? Age { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<ExamResult> ExamResults { get; set;} = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
