using MlcAccounting.Domain.UserAggregate.Entities;
using MlcAccounting.Infrastructure.UserRepository.Dtos;

namespace MlcAccounting.Infrastructure.UserRepository.Mappers;

public static class UserMapper
{
    public static IEnumerable<User> ToEntity(this IEnumerable<UserDto> dtos) => dtos.Select(_ => _.ToEntity()).ToList();

    public static User ToEntity(this UserDto dto) => new(dto.Name, dto.Password)
    {
        Id = dto.Id,
        CreatedAt = dto.CreatedAt,
        UpdatedAt = dto.UpdatedAt
    };
}