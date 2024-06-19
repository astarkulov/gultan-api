namespace Gultan.Application.StockData.Queries.GetTickers;

public record GetTickersQuery() : IRequest<StockDto[]>;

public class GetTickersQueryHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IRequestHandler<GetTickersQuery, StockDto[]>
{
    public async Task<StockDto[]> Handle(GetTickersQuery request, CancellationToken cancellationToken)
    {
        var tickers = await context.Stocks
            .AsQueryable()
            .Include(x => x.Forecasts)
            .OrderBy(x => x.Name)
            .ToArrayAsync(cancellationToken);

        var forecasts = await context.Forecasts
            .ToArrayAsync(cancellationToken);

        var mappedTickers = mapper.Map<StockDto[]>(tickers);

        foreach (var ticker in mappedTickers)
        {
            ticker.RecommendCount = ticker.DefaultRecommendCount;
            var forecast = forecasts.Where(x => x.StockId == ticker.Id).ToArray();
            if (forecast.Length > 0)
            {
                ticker.ForecastPrice = forecasts.Where(x => x.StockId == ticker.Id).Average(x => x.PredictedPrice);
            }
        }

        return mappedTickers
            .OrderByDescending(x => x.RecommendCount)
            .ThenByDescending(x => x.ForecastPrice)
            .ToArray();
    }
}