using AutoMapper;
using Gultan.Application.Auth.Commands.Login;
using Gultan.Application.Auth.Commands.Logout;
using Gultan.Application.Auth.Commands.Refresh;
using Gultan.Application.Auth.Commands.Register;
using Gultan.Application.Common.Contracts.Auth;
using Gultan.Application.Common.Dto;
using Gultan.Application.Common.Interfaces.Data;
using Gultan.Application.Common.Interfaces.Security;
using Gultan.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Gultan.API.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("CorsPolicy")]
public class AuthController(ISender sender, IApplicationDbContext context, IJwtProvider jwtProvider, IMapper mapper) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType<AuthResponse>(200)]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        var response = await sender.Send(new RegisterCommand(model.UserName, model.Email, model.Password));
        SetRefreshTokenInCookie(response.RefreshToken);

        return Ok(response);
    }

    [HttpPost("login")]
    [ProducesResponseType<AuthResponse>(200)]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var response = await sender.Send(new LoginCommand(model.UserName, model.Password));
        SetRefreshTokenInCookie(response.RefreshToken);

        return Ok(response);
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await sender.Send(new LogoutCommand(HttpContext.Request.Cookies["refreshToken"]));
        HttpContext.Response.Cookies.Delete("refreshToken");
        
        return Ok();
    }

    [HttpGet("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var response = await sender.Send(new RefreshCommand(HttpContext.Request.Cookies["refreshToken"]));
        SetRefreshTokenInCookie(response.RefreshToken);

        return Ok(response);
    }
    
    [Authorize]
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var response = await context.Users.AsQueryable().ToListAsync();

        return Ok(response);
    }

    [Authorize]
    [HttpGet("user")]
    public async Task<IActionResult> GetUser()
    {
        var payload = jwtProvider.ValidateRefreshToken(HttpContext.Request.Cookies["refreshToken"]);
        var userId = int.Parse(payload.GetValueOrDefault("userId"));

        var response = await context.Users.FirstOrDefaultAsync(x =>x.Id == userId);

        return Ok(mapper.Map<User, UserDto>(response));
    }
    

    private void SetRefreshTokenInCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.Now.AddDays(30)
        };
        HttpContext.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
    //
    // [HttpPost("activate")]
    // public async Task<IActionResult> Activation([FromQuery] string link)
    // {
    //     await sender.Send(new ActivateCommand(link));
    //
    //     return Ok();
    // }
}