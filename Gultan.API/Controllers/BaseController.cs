using Gultan.Application.Common.Constants;

namespace Gultan.API.Controllers;

public abstract class BaseController : ControllerBase
{
    protected int GetUserId()
    {
        return int.Parse(User.Claims.First(i => i.Type == JwtClaims.UserId).Value);
    }
}