using System.Reflection;
using Gultan.Application.Common.Behaviours;
using Gultan.Application.Common.Interfaces.Services;
using Gultan.Application.Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gultan.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });

        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}