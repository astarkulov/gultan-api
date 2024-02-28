using Gultan.Application.Common.Contracts.Auth;
using Gultan.Application.Common.Interfaces.Services;

namespace Gultan.Application.Auth.Commands;

public record LoginCommand(string UserName, string Password) : IRequest<AuthResponse>;

public class LoginCommandHandler(
    IPasswordHasher passwordHasher,
    IApplicationDbContext context,
    IJwtProvider jwtProvider,
    ITokenService tokenService,
    IMapper mapper) : IRequestHandler<LoginCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users
                       .FirstOrDefaultAsync(x => x.UserName == request.UserName, cancellationToken)
            ?? throw new Exception($"Пользователь {request.UserName} не существует");

        var result = passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!result)
        {
            throw new Exception("Пароль не совпадает");
        }

        var tokens = jwtProvider.GenerateTokens(mapper.Map<User, UserDto>(user));
        await tokenService.SaveToken(user, tokens.RefreshToken, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new AuthResponse(mapper.Map<User, UserDto>(user), tokens);
    }
}
