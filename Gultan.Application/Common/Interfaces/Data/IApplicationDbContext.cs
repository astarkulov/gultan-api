namespace Gultan.Application.Common.Interfaces.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Token> Tokens { get; set; }
    DbSet<Forecast> Forecasts { get; set; }
    DbSet<Stock> Stocks { get; set; }
    DbSet<StockPrice> StockPrices { get; set; }
    DbSet<Wallet> Wallets { get; set; }
    DbSet<WalletStock> WalletStocks { get; set; }
    DbSet<Goal> Goals { get; set; }
    DbSet<ForecastUpdate> ForecastUpdates { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}