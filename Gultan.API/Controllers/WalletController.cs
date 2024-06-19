using Gultan.Application.Common.Dto;
using Gultan.Application.WalletData.Commands.AddWallet;
using Gultan.Application.WalletData.Commands.AddWalletStocks;
using Gultan.Application.WalletData.Commands.UpdateSettings;
using Gultan.Application.WalletData.Commands.UpdateWalletStocks;
using Gultan.Application.WalletData.Queries.GetWalletById;
using Gultan.Application.WalletData.Queries.GetWalletGoals;
using Gultan.Application.WalletData.Queries.GetWallets;
using Gultan.Application.WalletData.Queries.GetWalletStocks;
using Microsoft.AspNetCore.Authorization;

namespace Gultan.API.Controllers;

public class WalletController(ISender sender) : BaseController
{
    [HttpPost("wallets/{name}")]
    public async Task<IActionResult> AddWallet([FromRoute] string name)
    {
        await sender.Send(new AddWalletCommand(name, GetUserId()));

        return NoContent();
    }

    [HttpGet("wallets")]
    public async Task<IActionResult> GetWallets()
    {
        var result = await sender.Send(new GetWalletsQuery());

        return Ok(result);
    }

    [HttpPost("wallet-stocks")]
    public async Task<IActionResult> AddWalletStocks([FromBody] AddWalletStocksCommand request)
    {
        await sender.Send(request);

        return NoContent();
    }

    [HttpGet("wallet-stocks")]
    public async Task<IActionResult> GetWalletStocks([FromQuery] GetWalletStocksQuery request)
    {
        var result = await sender.Send(request);

        return Ok(result);
    }

    [HttpPut("wallet-stocks")]
    public async Task<IActionResult> UpdateWalletStocks([FromBody] UpdateWalletStocksCommand request)
    {
        await sender.Send(request);

        return NoContent();
    }

    [HttpPut("wallet-settings")]
    public async Task<IActionResult> UpdateWalletSettings([FromBody] UpdateWalletSettingsCommand request)
    {
        await sender.Send(request);

        return NoContent();
    }

    [HttpGet("wallet-goals")]
    public async Task<IActionResult> GetWalletGoals()
    {
        var result = await sender.Send(new GetWalletGoalsQuery());

        return Ok(result);
    }

    [HttpGet("wallet-by-id")]
    public async Task<IActionResult> GetWalletById([FromQuery] GetWalletByIdQuery request)
    {
        var wallet = await sender.Send(request);

        return Ok(wallet);
    }
}