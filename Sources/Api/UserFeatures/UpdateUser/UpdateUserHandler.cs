using MediatR;
using MlcAccounting.Domain.UserAggregate;

namespace MlcAccounting.Api.UserFeatures.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserService _userService;

    public UpdateUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken) =>
        await _userService.UpdateAsync(request.ToEntity());
}