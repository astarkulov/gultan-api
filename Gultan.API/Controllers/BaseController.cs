using Gultan.Application.Common.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Gultan.API.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("CorsPolicy")]
[Authorize]
public abstract class BaseController : ControllerBase
{
    protected int GetUserId()
    {
        return int.Parse(User.Claims.First(i => i.Type == JwtClaims.UserId).Value);
    }
}