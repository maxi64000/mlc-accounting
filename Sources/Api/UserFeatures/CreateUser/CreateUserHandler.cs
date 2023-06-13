using MediatR;
using MlcAccounting.Domain.UserAggregate;

namespace MlcAccounting.Api.UserFeatures.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid?>
{
    private readonly IUserService _userService;

    public CreateUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Guid?> Handle(CreateUserCommand request, CancellationToken cancellationToken) =>
        await _userService.CreateAsync(request.ToEntity());
}