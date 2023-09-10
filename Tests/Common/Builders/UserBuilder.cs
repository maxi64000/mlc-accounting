using MlcAccounting.Domain.UserAggregate.Entities;

namespace MlcAccounting.Tests.Common.Builders;

public class UserBuilder
{
    private Guid _id;

    private string _name;

    private string _password;

    private DateTime _createdAt;

    private DateTime? _updatedAt;

    public UserBuilder()
    {
        _id = Guid.NewGuid();
        _name = "Name";
        _password = "Password";
        _createdAt = DateTime.Now;
        _updatedAt = DateTime.Now;
    }

    public UserBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public UserBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public UserBuilder WithPassword(string password)
    {
        _password = password;
        return this;
    }

    public UserBuilder WithCreatedAt(DateTime createdAt)
    {
        _createdAt = createdAt;
        return this;
    }

    public UserBuilder WithUpdatedAt(DateTime? updatedAt)
    {
        _updatedAt = updatedAt;
        return this;
    }

    public User Build() => new(_name, _password)
    {
        Id = _id,
        CreatedAt = _createdAt,
        UpdatedAt = _updatedAt
    };
}