using MlcAccounting.Domain.UserAggregate.Entities;

namespace MlcAccounting.Referential.UserFeatures.UpdateUser;

public static class UpdateUserMapper
{
    public static User ToEntity(this UpdateUserCommand command) => new(command.Name!, command.Password!)
    {
        Id = command.Id
    };
}