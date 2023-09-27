using MlcAccounting.Common.Integration.Entities;
using MlcAccounting.Common.Integration.Enums;

namespace MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;

public class UserIntegration
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid? PackageId { get; set; }

    public IntegrationStatus Status { get; set; } = IntegrationStatus.InProgress;

    public string? Name { get; set; }

    public string? Password { get; set; }

    public IEnumerable<Commentary> Commentaries { get; set; } = new List<Commentary>();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? TreatedAt { get; set; }
}