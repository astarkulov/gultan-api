using System.Security.Claims;

namespace Gultan.Application.Common.Interfaces.Security;

public interface IJwtProvider
{
    TokenDto GenerateTokens(UserDto user);
    public ClaimsPrincipal ValidateRefreshToken(string refreshToken);
    public ClaimsPrincipal ValidateAccessToken(string accessToken);
}