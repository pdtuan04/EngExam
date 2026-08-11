using Infrastructure.Repositories.SQLServer.DataContext;
using Infrastructure.Repositories.SQLServer_Read.DataContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ApplyMigrations
    {
        public static void UseApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;
            using var context = services.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            using var readContext = services.GetRequiredService<ApplicationDbReadContext>();
            readContext.Database.Migrate();
        }
    }
}