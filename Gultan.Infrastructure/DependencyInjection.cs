using Gultan.Application.Common.Interfaces.Email;
using Gultan.Application.Common.Interfaces.Services;
using Gultan.Application.Common.Services;
using Gultan.Infrastructure.Helpers.Email;
using Gultan.Infrastructure.Services;
using Gultan.Infrastructure.Services.StockDataService;
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
            .AddHttpClient()
            .AddDatabase(configuration)
            .AddSecurityServices(configuration)
            .AddServices(configuration);
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("GultanConnectionString");
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(opt =>
            opt.UseNpgsql(connectionString));
            // opt.UseSqlServer(connectionString));
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

    private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IStockDataService, StockDataService>();
        services.AddScoped<ICapitalOrganizeService, CapitalOrganizeService>();
        services.AddHostedService<StockDataBackgroundService>();
        services.Configure<StockDataOptions>(configuration.GetSection(nameof(StockDataOptions)));

        return services;
    }
}