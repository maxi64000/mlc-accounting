using MediatR;
using MlcAccounting.Referential.Domain.UserAggregate;
using MlcAccounting.Referential.Domain.UserAggregate.Entities;

namespace MlcAccounting.Referential.Api.UserFeatures.GetAllUsers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
{
    private readonly UserService _service;

    public GetAllUsersHandler(UserService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken) =>
        await _service.GetAllAsync(request.ToSpecification());
}