namespace Gultan.Application.WalletData.Commands.UpdateWallet;

public record UpdateWalletCommand(WalletDto Wallet) : IRequest;