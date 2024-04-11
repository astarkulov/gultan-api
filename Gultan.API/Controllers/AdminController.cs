using Gultan.Application.Admin.Commands.AddTickers;

namespace Gultan.API.Controllers;

public class AdminController(ISender sender) : BaseController
{
    [HttpPost("add-tickers")]
    public async Task<IActionResult> AddTickers([FromBody] AddTickersCommand request)
    {
        await sender.Send(request);
        
        return NoContent();
    }
}