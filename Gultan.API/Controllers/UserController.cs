﻿using Gultan.Application.Common.Dto;
using Gultan.Application.Users.Commands.ChangePassword;
using Gultan.Application.Users.Commands.Edit;
using Gultan.Application.Users.Queries;

namespace Gultan.API.Controllers;

public class UsersController(ISender sender) : BaseController
{
    [HttpGet("get-me")]
    public async Task<IActionResult> GetMe()
    {
        var result = await sender.Send(new GetMeQuery(GetUserId()));

        return Ok(result);
    }
    
    [HttpPatch("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto data)
    {
        await sender.Send(new ChangePasswordCommand(data, GetUserId()));
        
        return NoContent();
    }

    [HttpPatch("edit")]
    public async Task<IActionResult> Edit([FromBody] UserDto userDto)
    {
        await sender.Send(new EditCommand(userDto, GetUserId()));

        return NoContent();
    }
}