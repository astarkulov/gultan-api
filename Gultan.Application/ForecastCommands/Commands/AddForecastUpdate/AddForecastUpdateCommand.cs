namespace Gultan.Application.ForecastCommands.Commands.AddForecastUpdate;

public record AddForecastUpdateCommand(DateTime ForecastDate, string Tickers) : IRequest;

public class AddForecastUpdateCommandHandler(IApplicationDbContext context) : IRequestHandler<AddForecastUpdateCommand>
{
    public async Task Handle(AddForecastUpdateCommand request, CancellationToken cancellationToken)
    {
        var model = new ForecastUpdate()
        {
            ForecastDate = request.ForecastDate,
            Tickers = request.Tickers
        };

        context.ForecastUpdates.Add(model);

        await context.SaveChangesAsync(cancellationToken);
    }
}