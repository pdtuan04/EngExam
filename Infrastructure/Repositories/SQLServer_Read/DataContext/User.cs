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
    public class User : BaseEntity<Guid>
    {
        public int? Age { get; set; }
        public required string UserName { get; set; }
        public required string NormalizedUserName { get; set; }
        public required string Email { get; set; }
        public required string NormalizedEmail { get; set; }
        public string? ImageUrl { get; set; }
    }
}
