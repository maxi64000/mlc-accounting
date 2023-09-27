using MediatR;
using MlcAccounting.Referential.Domain.UserAggregate;
using MlcAccounting.Referential.Domain.UserAggregate.Entities;

namespace MlcAccounting.Referential.Api.UserFeatures.GetUser;

public class GetUserHandler : IRequestHandler<GetUserQuery, User?>
{
    private readonly UserService _service;

    public GetUserHandler(UserService service)
    {
        _service = service;
    }

    public async Task<User?> Handle(GetUserQuery request, CancellationToken cancellationToken) =>
        await _service.GetAsync(request.Id);
}