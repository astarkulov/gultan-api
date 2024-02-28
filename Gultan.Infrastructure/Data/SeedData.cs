using Microsoft.Extensions.DependencyInjection;

namespace Gultan.Infrastructure.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        // Применяем все миграции, которых еще нет в базе данных
        context.Database.Migrate();
    }
}