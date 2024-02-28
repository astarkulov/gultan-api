namespace Gultan.Application.Common.Contracts.Auth;

public class AuthResponse(UserDto user, TokenDto tokens)
{
    public UserDto User { get; set; } = user;
    public string RefreshToken { get; set; } = tokens.RefreshToken;
    public string AccessToken { get; set; } = tokens.AccessToken;
}