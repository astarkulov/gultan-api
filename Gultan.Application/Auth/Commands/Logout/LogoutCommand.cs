using Gultan.Application.Common.Exceptions.Auth;

namespace Gultan.Application.Auth.Commands.Logout;

public record LogoutCommand(string RefreshToken) : IRequest;

public class LogoutCommandHandler(IApplicationDbContext context) : IRequestHandler<LogoutCommand>
{
    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var token = await context.Tokens.FirstOrDefaultAsync(x => x.RefreshToken == request.RefreshToken,
                        cancellationToken)
                    ?? throw new UnAuthorizedException();
        context.Tokens.Remove(token);
        await context.SaveChangesAsync(cancellationToken);
    }
}