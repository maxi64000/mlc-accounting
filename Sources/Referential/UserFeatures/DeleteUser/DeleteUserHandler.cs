using MediatR;
using MlcAccounting.Domain.UserAggregate;

namespace MlcAccounting.Referential.UserFeatures.DeleteUser;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly UserService _service;

    public DeleteUserHandler(UserService service)
    {
        _service = service;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken) =>
        await _service.DeleteAsync(request.Id);
}