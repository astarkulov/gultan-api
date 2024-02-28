using Gultan.Application.Common.Constants;
using Gultan.Application.Common.Contracts.Auth;
using Gultan.Application.Common.Exceptions.Auth;
using Gultan.Application.Common.Interfaces.Services;

namespace Gultan.Application.Auth.Commands;

public record RefreshCommand(string RefreshToken) : IRequest<AuthResponse>;

public class RefreshCommandHandler(
    IApplicationDbContext context, 
    IJwtProvider jwtProvider, 
    ITokenService tokenService,
    IMapper mapper) : IRequestHandler<RefreshCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.RefreshToken)) throw new UnAuthorizedException();
        var userData = jwtProvider.ValidateRefreshToken(request.RefreshToken);
        await tokenService.FindToken(request.RefreshToken, cancellationToken);

        var userIdClaim = userData.Claims.First(x => x.Type == JwtClaims.UserId).Value;
        var user = await context.Users.FirstAsync(x => x.Id == int.Parse(userIdClaim), cancellationToken);
        var userDto = mapper.Map<User, UserDto>(user);
        var tokens = jwtProvider.GenerateTokens(userDto);
        await tokenService.SaveToken(user, tokens.RefreshToken, cancellationToken);

        return new AuthResponse(userDto, tokens);
    }
}