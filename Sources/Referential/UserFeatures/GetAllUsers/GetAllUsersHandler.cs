using MediatR;
using MlcAccounting.Domain.UserAggregate;
using MlcAccounting.Domain.UserAggregate.Entities;

namespace MlcAccounting.Referential.UserFeatures.GetAllUsers;

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