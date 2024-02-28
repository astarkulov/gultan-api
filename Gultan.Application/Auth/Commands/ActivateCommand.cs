namespace Gultan.Application.Auth.Commands;

public record ActivateCommand(string ActivationLink) : IRequest;

public class ActivationCommandHandler(
    IApplicationDbContext context) : IRequestHandler<ActivateCommand>
{
    
    public async Task Handle(ActivateCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(u => u.ActivationLink == request.ActivationLink, cancellationToken);
        if (user is null)
            throw new Exception("Неккоректная ссылка для активации");
        user.IsActivated = true;
        await context.SaveChangesAsync(cancellationToken);
    }
}