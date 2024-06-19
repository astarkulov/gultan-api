using Gultan.Application.Common.Dto;
using Gultan.Application.Common.ViewData;
using Gultan.Application.ForecastCommands.Commands.AddForecastUpdate;
using Gultan.Application.ForecastCommands.Query.GetForecastUpdates;
using Gultan.Application.StockData.Queries.GetTickers;
using Gultan.Application.StockData.Queries.GetWalletTickers;

namespace Gultan.API.Controllers;

public class StockDataController(ISender sender) : BaseController
{
    [HttpGet("tickers")]
    [ProducesResponseType<StockDto[]>(200)]
    public async Task<IActionResult> GetTickers()
    {
        var response = await sender.Send(new GetTickersQuery());

        return Ok(response);
    }
    
    [HttpGet("tickers-filtered")]
    [ProducesResponseType<CapitalOrganizedStock>(200)]
    public async Task<IActionResult> GetTickersFiltered([FromQuery] int[] existIds, [FromQuery] int walletId)
    {
        var response = await sender.Send(new GetWalletTickersQuery(existIds, walletId));

        return Ok(response);
    }

    [HttpPost("forecast-update")]
    public async Task<IActionResult> UpdateForecast(AddForecastUpdateCommand request)
    {
        await sender.Send(request);

        return NoContent();
    }

    [HttpGet("forecast-update")]
    public async Task<IActionResult> GetForecastUpdates()
    {
        var result = await sender.Send(new GetForecastUpdatesQuery());

        return Ok(result);
    }
}