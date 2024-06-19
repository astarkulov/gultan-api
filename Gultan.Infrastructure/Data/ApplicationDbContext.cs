using System.Reflection;

namespace Gultan.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Token> Tokens { get; set; }
    public DbSet<Forecast> Forecasts { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<StockPrice> StockPrices { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<WalletStock> WalletStocks { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<ForecastUpdate> ForecastUpdates { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<Goal>().HasData(
            new Goal { Id = 1, Name = "Образование" },
            new Goal { Id = 2, Name = "Покупка недвижимости" },
            new Goal { Id = 3, Name = "Путешествие" }
        );
        
        builder.Entity<Forecast>(entity =>
        {
            entity.Property(e => e.ForecastDate)
                .HasColumnType("date");
        });
    }
}