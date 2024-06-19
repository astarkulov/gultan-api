using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YahooQuotesApi;

namespace Gultan.Infrastructure.Services;

public class StockDataBackgroundService(IServiceScopeFactory scopeFactory, IMapper mapper) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var tickers = await context.Stocks.ToArrayAsync(stoppingToken);

            var yahooQuotes = new YahooQuotesBuilder().Build();
            foreach (var ticker in tickers)
            {
                try
                {
                    var quote = await yahooQuotes.GetAsync(ticker.Symbol, ct: stoppingToken);
                    ticker.LastPrice = quote?.RegularMarketPrice;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching data for {ticker}: {ex.Message}");
                }
            }
            await context.SaveChangesAsync(stoppingToken);

            await Task.Delay(1800000, stoppingToken);
        }
    }
}