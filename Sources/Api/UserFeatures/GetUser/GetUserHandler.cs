using MediatR;
using MlcAccounting.Domain.UserAggregate;
using MlcAccounting.Domain.UserAggregate.Entities;

namespace MlcAccounting.Api.UserFeatures.GetUser;

public class GetUserHandler : IRequestHandler<GetUserQuery, User?>
{
    private readonly IUserService _userService;

    public GetUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<User?> Handle(GetUserQuery request, CancellationToken cancellationToken) =>
        await _userService.GetAsync(request.Id);
}