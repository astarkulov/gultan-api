using Gultan.Application.Common.Exceptions.Auth;

namespace Gultan.Application.Users.Queries;

public record GetMeQuery(int UserId) : IRequest<UserDto>;

public class GetMeQueryHandler(
    IApplicationDbContext context,
    IMapper mapper) : IRequestHandler<GetMeQuery, UserDto>
{
    public async Task<UserDto> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken)
                   ?? throw new UnAuthorizedException();

        return mapper.Map<User, UserDto>(user);
    }
}