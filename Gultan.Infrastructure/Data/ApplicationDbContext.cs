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

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}