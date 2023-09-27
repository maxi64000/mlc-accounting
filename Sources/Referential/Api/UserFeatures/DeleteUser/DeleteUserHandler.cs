using MediatR;
using MlcAccounting.Referential.Domain.UserAggregate;

namespace MlcAccounting.Referential.Api.UserFeatures.DeleteUser;

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