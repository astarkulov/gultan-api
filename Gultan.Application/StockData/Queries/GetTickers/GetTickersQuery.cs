namespace Gultan.Application.StockData.Queries.GetTickers;

public record GetTickersQuery(int[]? ExistIds) : IRequest<StockDto[]>;

public class GetTickersQueryHandler(
    IApplicationDbContext context, 
    IMapper mapper
    ) : IRequestHandler<GetTickersQuery, StockDto[]>
{
    public async Task<StockDto[]> Handle(GetTickersQuery request, CancellationToken cancellationToken)
    {
        List<Stock> tickers;
        if (request.ExistIds is not null)
        {
            tickers = await context.Stocks
                .Where(x => !request.ExistIds.Contains(x.Id))
                .ToListAsync(cancellationToken);
        }
        else
        {
            tickers = await context.Stocks
                .ToListAsync(cancellationToken);
        }
      
        return mapper.Map<StockDto[]>(tickers.ToArray());
    }
}