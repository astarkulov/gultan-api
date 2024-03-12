using Gultan.Application.Common.Exceptions.Auth;

namespace Gultan.Application.Users.Commands.ChangePassword;

public record ChangePasswordCommand(ChangePasswordDto Data, int UserId) : IRequest;

public class ChangePasswordCommandHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher) : IRequestHandler<ChangePasswordCommand>
{
    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken)
                   ?? throw new UnAuthorizedException();
        var result = passwordHasher.Verify(request.Data.OldPassword, user.PasswordHash);

        if (!result)
            throw new InvalidPasswordException();

        user.PasswordHash = passwordHasher.Generate(request.Data.NewPassword);
        await context.SaveChangesAsync(cancellationToken);
    }
}