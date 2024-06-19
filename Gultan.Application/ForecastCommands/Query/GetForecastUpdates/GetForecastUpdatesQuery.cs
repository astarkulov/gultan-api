namespace Gultan.Application.ForecastCommands.Query.GetForecastUpdates;

public record GetForecastUpdatesQuery() : IRequest<ForecastUpdate[]> ;

public class GetForecastUpdatesQueryHandler(IApplicationDbContext context) : IRequestHandler<GetForecastUpdatesQuery, ForecastUpdate[]>
{
    public async Task<ForecastUpdate[]> Handle(GetForecastUpdatesQuery request, CancellationToken cancellationToken)
    {
        var forecastUpdates = await context.ForecastUpdates.ToArrayAsync(cancellationToken);

        return forecastUpdates;
    }
}