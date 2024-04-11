using Gultan.Application.Common.Dto;
using Gultan.Application.StockData.Queries.GetTickers;

namespace Gultan.API.Controllers;

public class StockDataController(ISender sender) : BaseController
{
    [HttpGet("tickers")]
    [ProducesResponseType<StockDto[]>(200)]
    public async Task<IActionResult> GetTickers([FromQuery] int[]? existIds = null)
    {
        var response = await sender.Send(new GetTickersQuery(existIds));

        return Ok(response);
    }
}