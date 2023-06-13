using MlcAccounting.Domain.UserAggregate.Entities;

namespace MlcAccounting.Infrastructure.UserRepository.Dtos;

public class UserDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public UserDto(User user)
    {
        Id = user.Id;
        Name = user.Name;
        Password = user.Password;
        CreatedAt = user.CreatedAt;
        UpdatedAt = user.UpdatedAt;
    }
}