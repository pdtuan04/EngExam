using Infrastructure.Repositories.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;

namespace EngExam.Extensions
{
    public static class ApplyMigrations
    {
        public static void UseApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();
            using ApplicationDbContext context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
        }
    }
}
