using Gultan.Application.Common.Exceptions.Auth;

namespace Gultan.Application.Users.Commands.Edit;

public record EditCommand(UserDto UserDto, int UserId) : IRequest;

public class EditCommandHandler(
    IApplicationDbContext context) : IRequestHandler<EditCommand>
{
    public async Task Handle(EditCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken)
                   ?? throw new UnAuthorizedException();

        user.Name = request.UserDto.Name;
        user.Surname = request.UserDto.Surname;
        user.PhoneNumber = request.UserDto.PhoneNumber;
    }
}