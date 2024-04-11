namespace Gultan.Application.WalletData.Commands.AddWalletStocks;

public record AddWalletStocksCommand(int WalletId, int[] StockIds) : IRequest;

public class AddWalletStocksCommandHandler(IApplicationDbContext context) : IRequestHandler<AddWalletStocksCommand>
{
    public async Task Handle(AddWalletStocksCommand request, CancellationToken cancellationToken)
    {
        var wallet = await context.Wallets
            .FirstOrDefaultAsync(x => x.Id == request.WalletId, cancellationToken);
        if (wallet is null)
            throw new Exception("Такого кошелька не существует");
        var stocks = await context.Stocks
            .Where(x => request.StockIds.Contains(x.Id))
            .ToListAsync(cancellationToken: cancellationToken);

        var newWalletStocks = stocks
            .Select(stock => new WalletStock() { WalletId = request.WalletId, Stock = stock })
            .ToList();
        context.WalletStocks.AddRange(newWalletStocks);
        await context.SaveChangesAsync(cancellationToken);
    }
}