using AutoMapper;
using Gultan.Application.Auth.Commands;
using Gultan.Application.Common.Constants;
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
        await sender.Send(new LogoutCommand(HttpContext.Request.Cookies["token"]));
        HttpContext.Response.Cookies.Delete("token");
        
        return Ok();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var response = await sender.Send(new RefreshCommand(HttpContext.Request.Cookies["token"]));
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
        var claims = jwtProvider.ValidateRefreshToken(HttpContext.Request.Cookies["token"]);
        var userId = int.Parse(claims.Claims.First(x => x.Type == JwtClaims.UserId).Value.ToString());

        var response = await context.Users.FirstOrDefaultAsync(x =>x.Id == userId);

        return Ok(mapper.Map<User, UserDto>(response));
    }
    

    private void SetRefreshTokenInCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            MaxAge = DateTime.UtcNow.AddDays(30).TimeOfDay,
            HttpOnly = true,
            Secure = true
        };
        HttpContext.Response.Cookies.Append("token", refreshToken, cookieOptions);
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