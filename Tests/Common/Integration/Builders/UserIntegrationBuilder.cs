using MlcAccounting.Common.Integration.Entities;
using MlcAccounting.Common.Integration.Enums;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;

namespace MlcAccounting.Tests.Common.Integration.Builders;

public class UserIntegrationBuilder
{
    private Guid _id;

    private Guid? _packageId;

    private IntegrationStatus _status;

    private string _name;

    private string _password;

    private IEnumerable<Commentary> _commentaries;

    private DateTime _createdAt;

    private DateTime? _treatedAt;

    public UserIntegrationBuilder()
    {
        _id = Guid.NewGuid();
        _packageId = Guid.NewGuid();
        _status = IntegrationStatus.InProgress;
        _name = "Name";
        _password = "Password";
        _commentaries = new List<Commentary>();
        _createdAt = DateTime.UtcNow;
        _treatedAt = DateTime.UtcNow;
    }

    public UserIntegrationBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public UserIntegrationBuilder WithPackageId(Guid? packageId)
    {
        _packageId = packageId;
        return this;
    }

    public UserIntegrationBuilder WithStatus(IntegrationStatus status)
    {
        _status = status;
        return this;
    }

    public UserIntegrationBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public UserIntegrationBuilder WithCommentaries(IEnumerable<Commentary> commentaries)
    {
        _commentaries = commentaries;
        return this;
    }

    public UserIntegrationBuilder WithPassword(string password)
    {
        _password = password;
        return this;
    }

    public UserIntegrationBuilder WithCreatedAt(DateTime createdAt)
    {
        _createdAt = createdAt;
        return this;
    }

    public UserIntegrationBuilder WithTreatedAt(DateTime? treatedAt)
    {
        _treatedAt = treatedAt;
        return this;
    }

    public UserIntegration Build() => new()
    {
        Id = _id,
        PackageId = _packageId,
        Status = _status,
        Name = _name,
        Password = _password,
        Commentaries = _commentaries,
        CreatedAt = _createdAt,
        TreatedAt = _treatedAt
    };
}