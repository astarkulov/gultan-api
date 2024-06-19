namespace Gultan.Application.WalletData.Queries.GetWalletById;

public record GetWalletByIdQuery(int WalletId) : IRequest<WalletDto>;

public class GetWalletByIdQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetWalletByIdQuery, WalletDto>
{
    public async Task<WalletDto> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
    {
        var wallet = await context.Wallets
                         .Include(x => x.Goal)
                         .FirstOrDefaultAsync(x => x.Id == request.WalletId, cancellationToken)
                     ?? throw new Exception($"Кошелек с id:{request.WalletId} не найден");
        
        var walletStocks = await context.WalletStocks
            .Include(x => x.Stock)
            .Where(x => x.WalletId == wallet.Id)
            .ToArrayAsync(cancellationToken);

        var mappedWallet = mapper.Map<WalletDto>(wallet);

        mappedWallet.Profit = walletStocks.Aggregate<WalletStock, decimal?>(0m,
            (current, item) => current + (item.Stock.LastPrice * item.Quantity - item.PurchasePrice * item.Quantity));

        return mappedWallet;
    }
}