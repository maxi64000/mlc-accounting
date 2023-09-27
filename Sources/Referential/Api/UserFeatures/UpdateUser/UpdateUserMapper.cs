using MlcAccounting.Referential.Domain.UserAggregate.Entities;

namespace MlcAccounting.Referential.Api.UserFeatures.UpdateUser;

public static class UpdateUserMapper
{
    public static User ToEntity(this UpdateUserCommand command) => new(command.Name!, command.Password!)
    {
        Id = command.Id
    };
}