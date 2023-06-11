namespace MlcAccounting.Domain.UserAggregate.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public string Password { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public User(string name, string password)
    {
        Name = name;
        Password = password;
    }
}