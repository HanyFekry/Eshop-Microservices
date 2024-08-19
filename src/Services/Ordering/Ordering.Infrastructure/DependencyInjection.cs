using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection")!;

            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.AddInterceptors(new AuditableEntityInterceptor());
                opt.UseSqlServer(connection);
            });
            return services;
        }

    }
}
