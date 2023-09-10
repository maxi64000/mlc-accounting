using MediatR;
using MlcAccounting.Domain.UserAggregate;

namespace MlcAccounting.Referential.UserFeatures.UpdateUser;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly UserService _service;

    public UpdateUserHandler(UserService service)
    {
        _service = service;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken) =>
        await _service.UpdateAsync(request.ToEntity());
}