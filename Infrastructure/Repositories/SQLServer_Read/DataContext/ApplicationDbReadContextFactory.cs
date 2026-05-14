using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SQLServer_Read.DataContext
{
    public class ApplicationDbReadContextFactory : IDesignTimeDbContextFactory<ApplicationDbReadContext>
    {
        public ApplicationDbReadContext CreateDbContext(string[] args)
        {   
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbReadContext>();
            optionsBuilder.UseSqlServer(@"Server=TUAN;Database=ENG_READ;Trusted_Connection=True;TrustServerCertificate=True");
            return new ApplicationDbReadContext(optionsBuilder.Options);
        }
    }
}
