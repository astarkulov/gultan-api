using Gultan.Application.Common.Contracts.Auth;
using Gultan.Application.Common.Exceptions.Auth;
using Gultan.Application.Common.Interfaces.Email;
using Gultan.Application.Common.Interfaces.Services;

namespace Gultan.Application.Auth.Commands;

public record RegisterCommand(string UserName, string Email, string Password) : IRequest<AuthResponse>;

public class RegisterCommandHandler(
    IPasswordHasher passwordHasher,
    IApplicationDbContext context,
    IEmailService emailService,
    ITokenService tokenService,
    IJwtProvider jwtProvider,
    IMapper mapper)
    : IRequestHandler<RegisterCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var candidate = await context.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (candidate is not null)
            throw new UserAlreadyExistsException(request.Email);

        var hashedPassword = passwordHasher.Generate(request.Password);
        //var activationLink = Guid.NewGuid().ToString();

        var user = new User()
        {
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = hashedPassword,
            //ActivationLink = activationLink
        };
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        //emailService.SendActivationMail(request.Email, activationLink);
        
        var tokens = jwtProvider.GenerateTokens(mapper.Map<User, UserDto>(user));
        await tokenService.SaveToken(user, tokens.RefreshToken, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new AuthResponse(mapper.Map<User, UserDto>(user), tokens);
    }
}