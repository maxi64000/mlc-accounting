using MlcAccounting.Common.Integration.Entities;
using MlcAccounting.Common.Integration.Enums;

namespace MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Entities;

public class UserPackageIntegration
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public IntegrationStatus Status { get; set; } = IntegrationStatus.InProgress;

    public IEnumerable<Commentary> Commentaries { get; set; } = new List<Commentary>();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}