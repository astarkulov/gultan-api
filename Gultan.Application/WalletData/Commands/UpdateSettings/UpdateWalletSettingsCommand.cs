using Gultan.Domain.Enums;

namespace Gultan.Application.WalletData.Commands.UpdateSettings;

public record UpdateWalletSettingsCommand(int WalletId, int GoalId, RiskLevel RiskLevel, int SharePurchaseLimit, decimal Capital) : IRequest;

public class UpdateWalletSettingsCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateWalletSettingsCommand>
{
    public async Task Handle(UpdateWalletSettingsCommand request, CancellationToken cancellationToken)
    {
        var wallet = await context.Wallets.FirstOrDefaultAsync(x => x.Id == request.WalletId, cancellationToken) ??
                       new Wallet();

        wallet.GoalId = request.GoalId;
        wallet.RiskLevel = request.RiskLevel;
        wallet.Capital = request.Capital;
        wallet.SharePurchaseLimit = request.SharePurchaseLimit;
        context.Wallets.Update(wallet);
        await context.SaveChangesAsync(cancellationToken);
    }
}