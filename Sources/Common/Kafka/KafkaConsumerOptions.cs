namespace MlcAccounting.Common.Kafka;

public class KafkaConsumerOptions
{
    public string? Broker { get; set; }

    public string? GroupId { get; set; }

    public string? Topic { get; set; }
}