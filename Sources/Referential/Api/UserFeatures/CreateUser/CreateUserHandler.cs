using MediatR;
using MlcAccounting.Referential.Domain.UserAggregate;

namespace MlcAccounting.Referential.Api.UserFeatures.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid?>
{
    private readonly UserService _service;

    public CreateUserHandler(UserService service)
    {
        _service = service;
    }

    public async Task<Guid?> Handle(CreateUserCommand request, CancellationToken cancellationToken) =>
        await _service.CreateAsync(request.ToEntity());
}