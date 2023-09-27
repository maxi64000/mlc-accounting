using Confluent.Kafka;
using Microsoft.Extensions.Options;
using MlcAccounting.Common.Kafka;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Abstractions;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;
using System.Text.Json;

namespace MlcAccounting.Integration.Infrastructure.Consumers;

public class UserIntegrationKafkaConsumer : IUserIntegrationConsumer
{
    private readonly IConsumer<Null, string> _consumer;

    public UserIntegrationKafkaConsumer(IOptions<UserIntegrationKafkaConsumerOptions> options)
    {
        _consumer = new ConsumerBuilder<Null, string>(new ConsumerConfig
        {
            BootstrapServers = options.Value.Broker,
            GroupId = options.Value.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        }).Build();

        _consumer.Subscribe(options.Value.Topic);
    }

    public UserIntegration Consume() =>
        JsonSerializer.Deserialize<UserIntegration>(_consumer.Consume().Message.Value)!;

    public void Commit() =>
        _consumer.Commit();
}

public class UserIntegrationKafkaConsumerOptions : KafkaConsumerOptions { }