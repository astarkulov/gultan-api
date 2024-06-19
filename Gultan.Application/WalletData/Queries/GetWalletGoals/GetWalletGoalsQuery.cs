namespace Gultan.Application.WalletData.Queries.GetWalletGoals;

public record GetWalletGoalsQuery : IRequest<GoalDto[]>;

public class GetWalletGoalsQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetWalletGoalsQuery, GoalDto[]>
{
    public async Task<GoalDto[]> Handle(GetWalletGoalsQuery request, CancellationToken cancellationToken)
    {
        var goals = await context.Goals.OrderBy(x => x.Id).ToArrayAsync(cancellationToken);

        return mapper.Map<GoalDto[]>(goals);
    }
}