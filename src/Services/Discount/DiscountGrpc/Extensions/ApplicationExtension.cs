using DiscountGrpc.Data;
using Microsoft.EntityFrameworkCore;

namespace DiscountGrpc.Extensions
{
    public static class ApplicationExtension
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            return app;
        }
    }
}
