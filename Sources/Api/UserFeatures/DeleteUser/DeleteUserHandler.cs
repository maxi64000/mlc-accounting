using MediatR;
using MlcAccounting.Domain.UserAggregate;

namespace MlcAccounting.Api.UserFeatures.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserService _userService;

    public DeleteUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken) =>
        await _userService.DeleteAsync(request.Id);
}