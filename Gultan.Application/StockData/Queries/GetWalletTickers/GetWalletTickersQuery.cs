using Gultan.Application.Common.Services;
using Gultan.Application.Common.ViewData;

namespace Gultan.Application.StockData.Queries.GetWalletTickers;

public record GetWalletTickersQuery(int[] ExistIds, int WalletId) : IRequest<CapitalOrganizedStock>;

public class GetWalletTickersQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    ICapitalOrganizeService capitalOrganizeService
) : IRequestHandler<GetWalletTickersQuery, CapitalOrganizedStock>
{
    public async Task<CapitalOrganizedStock> Handle(GetWalletTickersQuery request, CancellationToken cancellationToken)
    {
        var wallet = await context.Wallets
                         .FirstOrDefaultAsync(x => x.Id == request.WalletId, cancellationToken)
                     ?? throw new Exception("Кошелек не найден");

        if (wallet.GoalId is null) throw new Exception("Выбери цель");

        var tickers = context.Stocks
            .Where(x => !request.ExistIds.Contains(x.Id))
            .OrderBy(x => x.Name);
        if (wallet.GoalId == 1)
            tickers = tickers.OrderByDescending(x => x.Short);
        if (wallet.GoalId == 2)
            tickers = tickers.OrderByDescending(x => x.Middle);
        if (wallet.GoalId == 3)
            tickers = tickers.OrderByDescending(x => x.Long);

        tickers = tickers.OrderByDescending(x => x.RiskLevel == wallet.RiskLevel);

        var capitalOrganizedStocks = await capitalOrganizeService.MaxProfit(
            mapper.Map<List<StockDto>>(tickers.ToList()), wallet.Capital,
            wallet.SharePurchaseLimit, wallet.GoalId);

        var purchasesListId = capitalOrganizedStocks.purchases.Select(x => x.stockId);

        tickers = tickers.OrderBy(t => !purchasesListId.Contains(t.Id)).ThenBy(t => t.Id);

        var tickersModel = await tickers.ToArrayAsync(cancellationToken);

        var tickersDto = mapper.Map<StockDto[]>(tickersModel);

        foreach (var item in capitalOrganizedStocks.purchases)
        {
            tickersDto.First(x => x.Id == item.stockId).RecommendCount = item.count;
        }
        
        var forecasts = await context.Forecasts
            .ToArrayAsync(cancellationToken);
        
        foreach (var ticker in tickersDto)
        {
            var forecast = forecasts.Where(x => x.StockId == ticker.Id).ToArray();
            if (forecast.Length > 0)
            {
                ticker.ForecastPrice = forecast.Average(x => x.PredictedPrice);
            }
        }

        return new CapitalOrganizedStock(){MaxProfit = capitalOrganizedStocks.maxProfit, Stocks = tickersDto};
    }
}