using Confluent.Kafka;
using Microsoft.Extensions.Options;
using MlcAccounting.Common.Kafka;
using MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Abstractions;
using MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Entities;
using System.Text.Json;

namespace MlcAccounting.Integration.Infrastructure.Consumers;

public class UserPackageIntegrationKafkaConsumer : IUserPackageIntegrationConsumer
{
    private readonly IConsumer<Null, string> _consumer;

    public UserPackageIntegrationKafkaConsumer(IOptions<UserPackageIntegrationKafkaConsumerOptions> options)
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

    public UserPackageIntegration Consume() =>
        JsonSerializer.Deserialize<UserPackageIntegration>(_consumer.Consume().Message.Value)!;

    public void Commit() =>
        _consumer.Commit();
}

public class UserPackageIntegrationKafkaConsumerOptions : KafkaConsumerOptions { }