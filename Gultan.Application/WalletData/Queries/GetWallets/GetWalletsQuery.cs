namespace Gultan.Application.WalletData.Queries.GetWallets;

public record GetWalletsQuery() : IRequest<WalletDto[]>;

public class GetWalletsQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetWalletsQuery, WalletDto[]>
{
    public async Task<WalletDto[]> Handle(GetWalletsQuery request, CancellationToken cancellationToken)
    {
        var wallets = await context.Wallets.AsQueryable().ToArrayAsync(cancellationToken);

        return mapper.Map<WalletDto[]>(wallets);
    }
}