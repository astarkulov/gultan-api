namespace Gultan.Application.WalletData.Commands.UpdateWalletStocks;

public record UpdateWalletStocksCommand(WalletStockDto[] WalletStocks, int WalletId) : IRequest;

public class UpdateWalletStocksCommandHandler(IApplicationDbContext context, IMapper mapper) 
    : IRequestHandler<UpdateWalletStocksCommand>
{
    public async Task Handle(UpdateWalletStocksCommand request, CancellationToken cancellationToken)
    {
        var newModels = mapper.Map<WalletStock[]>(request.WalletStocks);
        var existingModels = await context.WalletStocks
            .Where(ws => ws.WalletId == request.WalletId)
            .ToListAsync(cancellationToken);

        var modelsToUpdate = newModels.Where(nm => existingModels.Select(em => em.Id).Contains(nm.Id)).ToList();
        var modelsToDelete = existingModels.Where(em => newModels.All(nm => nm.Id != em.Id)).ToList();

        foreach (var modelToUpdate in modelsToUpdate)
        {
            var existingModel = existingModels.First(em => em.Id == modelToUpdate.Id);
            context.WalletStocks.Entry(existingModel).CurrentValues.SetValues(modelToUpdate);
        }

        context.WalletStocks.RemoveRange(modelsToDelete);

        await context.SaveChangesAsync(cancellationToken);
    }
}