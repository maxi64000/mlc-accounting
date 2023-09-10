using MlcAccounting.Domain.UserIntegrationAggregate.Enums;

namespace MlcAccounting.Domain.UserIntegrationAggregate.Entities;

public class UserIntegration
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public IntegrationStatus Status { get; set; } = IntegrationStatus.InProgress;

    public string? Name { get; set; }

    public string? Password { get; set; }

    public IEnumerable<Commentary> Commentaries { get; set; } = new List<Commentary>();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}