namespace Gultan.Application.WalletData.Queries.GetWallets;

public record GetWalletsQuery() : IRequest<WalletDto[]>;

public class GetWalletsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser)
    : IRequestHandler<GetWalletsQuery, WalletDto[]>
{
    public async Task<WalletDto[]> Handle(GetWalletsQuery request, CancellationToken cancellationToken)
    {
        var wallets = await context.Wallets
            .Include(x => x.Goal)
            .Where(x => x.UserId == currentUser.UserId)
            .ToArrayAsync(cancellationToken);

        var mappedWallets = mapper.Map<WalletDto[]>(wallets);

        foreach (var wallet in mappedWallets)
        {
            wallet.Profit = await GetProfit(wallet.Id);
        }

        return mappedWallets;
    }

    private async Task<decimal?> GetProfit(int walletId)
    {
        var walletStocks = await context.WalletStocks
            .Include(x => x.Stock)
            .Where(x => x.WalletId == walletId)
            .ToArrayAsync();

        return walletStocks.Aggregate<WalletStock, decimal?>(0m,
            (current, item) => current + (item.Stock.LastPrice * item.Quantity - item.PurchasePrice * item.Quantity));
    }
}