using MlcAccounting.Domain.UserIntegrationAggregate.Enums;

namespace MlcAccounting.Domain.UserIntegrationAggregate.Entities;

public class Commentary
{
    public CommentaryType Type { get; set; }

    public string Message { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Commentary(CommentaryType type, string message)
    {
        Type = type;
        Message = message;
    }
}