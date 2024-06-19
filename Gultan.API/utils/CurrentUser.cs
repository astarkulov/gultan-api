using Gultan.Application.Common.Constants;
using Gultan.Application.Common.Interfaces.Data;

namespace Gultan.API.utils;

public class CurrentUser : ICurrentUser
{
    public int UserId { get; set; }
    
    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor.HttpContext == null)
        {
            throw new ArgumentException("HttpContext is null");
        }

        UserId = int.Parse(httpContextAccessor.HttpContext.User.Claims.First(i => i.Type == JwtClaims.UserId).Value);
    }
}