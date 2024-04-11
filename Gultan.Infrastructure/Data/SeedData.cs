using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gultan.Infrastructure.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        // Применяем все миграции, которых еще нет в базе данных
        context.Database.Migrate();
        FillForecast(context);
    }

    private static async void FillForecast(IApplicationDbContext dbContext)
    {
        if (dbContext.Forecasts.Any()) return;
        const string filePath = "../forecasts.csv";

        var forecasts = await ReadForecastsFromFile(filePath);
        var source = new CancellationTokenSource();
        await UpdateForecastsInDatabase(forecasts, dbContext, source.Token);
    }

    private static async Task<Dictionary<string, List<(DateTime Date, decimal PredictedPrice)>>> ReadForecastsFromFile(
        string filePath)
    {
        var forecasts = new Dictionary<string, List<(DateTime Date, decimal PredictedPrice)>>();

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            var records = csv.GetRecords<dynamic>().ToList();

            foreach (var record in records)
            {
                var recordDict = (IDictionary<string, object>)record;

                var date = DateTime.Parse(recordDict["date"].ToString()).ToUniversalTime();

                foreach (var kvp in recordDict)
                {
                    if (kvp.Key == "date") continue;

                    if (!forecasts.TryGetValue(kvp.Key, out var value))
                    {
                        value = new List<(DateTime Date, decimal PredictedPrice)>();
                        forecasts[kvp.Key] = value;
                    }

                    var predictedPrice = decimal.Parse(kvp.Value.ToString() ?? string.Empty, CultureInfo.InvariantCulture);
                    value.Add((date, predictedPrice));
                }
            }
        }

        return forecasts;
    }

    private static async Task UpdateForecastsInDatabase(
        Dictionary<string, List<(DateTime Date, decimal PredictedPrice)>> forecasts, IApplicationDbContext dbContext, CancellationToken cancellationToken)
    {
        var stocks = await dbContext.Stocks.ToListAsync(cancellationToken: cancellationToken);

        foreach (var (ticker, value) in forecasts)
        {
            var stock = stocks.FirstOrDefault(s => s.Symbol == ticker);

            if (stock == null)
            {
                Console.WriteLine($"Stock with ticker {ticker} not found in database.");
                continue;
            }

            foreach (var forecastEntity in from forecast in value
                     let predictedValue = (forecast.PredictedPrice / 1000m) * stock.LastPrice
                     select new Forecast
                     {
                         AdminId = 4,
                         StockId = stock.Id,
                         ForecastDate = DateTime.SpecifyKind(forecast.Date, DateTimeKind.Utc),
                         PredictedPrice = predictedValue
                     })
            {
                dbContext.Forecasts.Add(forecastEntity);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}