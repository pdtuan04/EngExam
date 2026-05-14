using Infrastructure.Repositories.SQLServer.DataContext;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.EntityFrameworkCore;

namespace EngExam.Extensions
{
    public static class ApplyMigrations
    {
        public static void UseApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();
            using ApplicationDbContext context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            using IServiceScope readServiceScope = app.ApplicationServices.CreateScope();
            using ApplicationDbReadContext readContext = readServiceScope.ServiceProvider.GetRequiredService<ApplicationDbReadContext>();
            context.Database.Migrate();
            readContext.Database.Migrate();
        }
    }
}
