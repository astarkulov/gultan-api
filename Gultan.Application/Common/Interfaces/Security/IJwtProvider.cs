using System.Security.Claims;

namespace Gultan.Application.Common.Interfaces.Security;

public interface IJwtProvider
{
    public TokenDto GenerateTokens(UserDto user);
    Dictionary<string,string> ValidateRefreshToken(string refreshToken);
    Dictionary<string,string> ValidateAccessToken(string accessToken);
}