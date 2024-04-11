using Gultan.Application.Common.Interfaces.Services;

namespace Gultan.Application.Admin.Commands.AddTickers;

public record AddTickersCommand(string[] Tickers) : IRequest;

public class AddTickersCommandHandler(
    IStockDataService stockDataService, 
    IMapper mapper,
    IApplicationDbContext context
    ) : IRequestHandler<AddTickersCommand>
{
    public async Task Handle(AddTickersCommand request, CancellationToken cancellationToken)
    {
        var stocks = await context.Stocks.AsQueryable().ToListAsync(cancellationToken: cancellationToken);
        var result = await stockDataService.GetStockData(request.Tickers.ToList());
        var temp = result.Where(x => !stocks.Select(u => u.Symbol).Contains(x.Symbol)).ToArray();

        var newStocks = mapper.Map<Stock[]>(temp);
        
        context.Stocks.AddRange(newStocks);
        await context.SaveChangesAsync(cancellationToken);
    }
}