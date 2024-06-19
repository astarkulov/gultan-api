using Gultan.Application.Common.Services;

namespace Gultan.Infrastructure.Services.StockDataService;

public class CapitalOrganizeService(IApplicationDbContext context) : ICapitalOrganizeService
{
    public async Task<(decimal maxProfit, List<(int stockId, int count)> purchases)> MaxProfit(List<StockDto> stocks, decimal? capital, int k, int? goalId)
    {
        var stockWithForecast = await DataPreparation(stocks, goalId);
        stockWithForecast = stockWithForecast.Where(x => x.ProfitPerStock > 0).ToList();
        
        int n = stockWithForecast.Count;
        int capitalInt = (int)(capital * 100); 
        decimal[] dp = new decimal[capitalInt + 1];
        Dictionary<int, int>[] purchases = new Dictionary<int, int>[capitalInt + 1];

        for (int i = 0; i <= capitalInt; i++)
        {
            purchases[i] = new Dictionary<int, int>();
        }

        foreach (var stock in stockWithForecast)
        {
            int stockCostInt = (int)(stock.LastPrice * 100);
            for (int count = 1; count <= k; count++)
            {
                for (int j = capitalInt; j >= stockCostInt; j--)
                {
                    if (dp[j] < dp[j - stockCostInt] + stock.ProfitPerStock)
                    {
                        dp[j] = dp[j - stockCostInt] + stock.ProfitPerStock;

                        purchases[j] = new Dictionary<int, int>(purchases[j - stockCostInt]);
                        if (purchases[j].ContainsKey(stock.Id))
                        {
                            purchases[j][stock.Id]++;
                        }
                        else
                        {
                            purchases[j][stock.Id] = 1;
                        }
                    }
                }
            }
        }

        List<(int stockId, int count)> resultPurchases = new List<(int stockId, int count)>();
        foreach (var kvp in purchases[capitalInt])
        {
            resultPurchases.Add((kvp.Key, kvp.Value));
        }

        return (dp[capitalInt], resultPurchases);
    }

    private async Task<List<StockWithForecast>> DataPreparation(List<StockDto> stocks, int? goalId)
    {
        var endDate = goalId switch
        {
            1 => DateTime.Now.AddDays(30),
            2 => DateTime.Now.AddDays(100),
            _ => DateTime.Now.AddDays(200)
        };

        var stocksIds = stocks.Select(s => s.Id).ToArray();

        var forecasts = await context.Forecasts
            .Where(x => stocksIds.Contains(x.StockId) 
                        && x.ForecastDate >= DateTime.Now && x.ForecastDate <= endDate)
            .GroupBy(f => f.StockId)
            .Select(g => new StockWithForecast
            { 
                Id = g.Key,
                ProfitPerStock = g.Average(f => f.PredictedPrice)
            })
            .ToListAsync();

        foreach (var item in forecasts)
        {
            item.LastPrice = stocks.First(x => x.Id == item.Id).LastPrice;
            item.ProfitPerStock -= stocks.First(x => x.Id == item.Id).LastPrice;
        }

        return forecasts;
    }

    private class StockWithForecast
    {
        public int Id { get; set; }
        public decimal LastPrice { get; set; }
        public decimal ProfitPerStock { get; set; }
    }
}