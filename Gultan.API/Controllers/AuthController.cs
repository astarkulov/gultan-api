using Gultan.Application.Auth.Commands.Login;
using Gultan.Application.Auth.Commands.Logout;
using Gultan.Application.Auth.Commands.Refresh;
using Gultan.Application.Auth.Commands.Register;
using Gultan.Application.Common.Contracts.Auth;

namespace Gultan.API.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("CorsPolicy")]
public class AuthController(ISender sender) : ControllerBase
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