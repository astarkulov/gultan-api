namespace Gultan.Application.WalletData.Commands.AddWallet;

public record AddWalletCommand(string Name, int UserId) : IRequest;

public class AddWalletCommandHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<AddWalletCommand>
{
    public async Task Handle(AddWalletCommand request, CancellationToken cancellationToken)
    {
        var wallet = new Wallet()
        {
            Name = request.Name,
            UserId = request.UserId,
            CreatedAt = DateTime.UtcNow
        };
        context.Wallets.Add(wallet);
        await context.SaveChangesAsync(cancellationToken);
    }
}