using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // register MediatR handlers from this assembly
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            // register validators
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            // register pipeline behavior for validation
            services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(Behaviors.ValidationBehavior<,>));

            return services;
        }
    }
}
