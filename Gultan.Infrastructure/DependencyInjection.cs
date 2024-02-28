using Gultan.Application.Common.Interfaces.Email;
using Gultan.Infrastructure.Helpers.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gultan.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddSecurityServices(configuration);
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("GultanConnectionString");
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));
        return services;
    }

    private static IServiceCollection AddSecurityServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IEmailService, EmailService>();
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        services.Configure<SmtpOptions>(configuration.GetSection(nameof(SmtpOptions)));

        return services;
    }
}