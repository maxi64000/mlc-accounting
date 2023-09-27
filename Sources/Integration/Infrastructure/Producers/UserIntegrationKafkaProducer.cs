using Confluent.Kafka;
using Microsoft.Extensions.Options;
using MlcAccounting.Common.Kafka;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Abstractions;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;
using System.Text.Json;

namespace MlcAccounting.Integration.Infrastructure.Producers;

public class UserIntegrationKafkaProducer : IUserIntegrationProducer
{
    private readonly UserIntegrationKafkaProducerOptions _options;

    private readonly IProducer<Null, string> _producer;

    public UserIntegrationKafkaProducer(IOptions<UserIntegrationKafkaProducerOptions> options)
    {
        _options = options.Value;

        _producer = new ProducerBuilder<Null, string>(new ProducerConfig
        {
            BootstrapServers = _options.Broker
        }).Build();
    }

    public async Task ProduceAsync(UserIntegration message) =>
        await _producer.ProduceAsync(_options.Topic, new Message<Null, string> { Value = JsonSerializer.Serialize(message) });
}

public class UserIntegrationKafkaProducerOptions : KafkaProducerOptions { }