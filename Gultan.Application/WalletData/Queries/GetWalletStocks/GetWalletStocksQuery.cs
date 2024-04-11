namespace Gultan.Application.WalletData.Queries.GetWalletStocks;

public record GetWalletStocksQuery(int WalletId) : IRequest<WalletStockDto[]>;

public class GetWalletStocksQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetWalletStocksQuery, WalletStockDto[]>
{
    public async Task<WalletStockDto[]> Handle(GetWalletStocksQuery request, CancellationToken cancellationToken)
    {
        var result = await context.WalletStocks
            .Where(x => x.WalletId == request.WalletId)
            .Include(x => x.Stock)
            .Include(x => x.Wallet)
            .ToArrayAsync(cancellationToken);

        return mapper.Map<WalletStockDto[]>(result);
    }
}