namespace Gultan.Application.Common.Interfaces.Services;

public interface ITokenService
{
    Task SaveToken(User user, string refreshToken, CancellationToken cancellationToken);
    Task FindToken(string refreshToken, CancellationToken cancellationToken);
}