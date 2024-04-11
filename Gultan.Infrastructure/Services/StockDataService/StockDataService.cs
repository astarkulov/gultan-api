using AutoMapper;
using Gultan.Application.Common.Interfaces.Services;
using YahooQuotesApi;

namespace Gultan.Infrastructure.Services.StockDataService;

public class StockDataService(IMapper mapper)
    : IStockDataService
{
    public async Task<StockDto[]> GetStockData(List<string> tickers)
    {
        var stocks = new List<StockDto>();
        var yahooQuotes = new YahooQuotesBuilder().Build();
        foreach (var ticker in tickers)
        {
            try
            {
                var quote = await yahooQuotes.GetAsync(ticker);
                var stock = mapper.Map<StockDto>(quote);
                stocks.Add(stock);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data for {ticker}: {ex.Message}");
            }
        }

        return stocks.ToArray();
    }
}