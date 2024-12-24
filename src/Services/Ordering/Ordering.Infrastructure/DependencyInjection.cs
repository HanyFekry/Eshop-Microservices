using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection")!;
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            services.AddScoped<SlowQueryInterceptor>();
            services.AddDbContext<ApplicationDbContext>((sp, opt) =>
            {
                opt.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                opt.AddInterceptors(sp.GetRequiredService<SlowQueryInterceptor>());
                opt.UseSqlServer(connection);
            });

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }

    }
}
