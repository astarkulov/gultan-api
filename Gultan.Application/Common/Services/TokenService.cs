using Gultan.Application.Common.Constants;
using Gultan.Application.Common.Exceptions.Auth;
using Gultan.Application.Common.Interfaces.Services;

namespace Gultan.Application.Common.Services;

public class TokenService(IApplicationDbContext context, IJwtProvider jwtProvider, IMapper mapper) : ITokenService
{
    public async Task SaveToken(User user, string refreshToken, CancellationToken cancellationToken)
    {
        var token = await context.Tokens.FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);
        if (token is not null)
            token.RefreshToken = refreshToken;
        else
        {
            var newToken = new Token
            {
                User = user,
                RefreshToken = refreshToken
            };
            await context.Tokens.AddAsync(newToken, cancellationToken);
        }
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task FindToken(string refreshToken, CancellationToken cancellationToken)
    {
        var result = await context.Tokens.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken, cancellationToken)
                     ?? throw new UnAuthorizedException();
    }
}