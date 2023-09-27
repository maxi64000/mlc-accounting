using Confluent.Kafka;
using Microsoft.Extensions.Options;
using MlcAccounting.Common.Kafka;
using MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Abstractions;
using MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Entities;
using System.Text.Json;

namespace MlcAccounting.Integration.Infrastructure.Producers;

public class UserPackageIntegrationKafkaProducer : IUserPackageIntegrationProducer
{
    private readonly UserPackageIntegrationKafkaProducerOptions _options;

    private readonly IProducer<Null, string> _producer;

    public UserPackageIntegrationKafkaProducer(IOptions<UserPackageIntegrationKafkaProducerOptions> options)
    {
        _options = options.Value;

        _producer = new ProducerBuilder<Null, string>(new ProducerConfig
        {
            BootstrapServers = _options.Broker
        }).Build();
    }

    public async Task ProduceAsync(UserPackageIntegration message) =>
        await _producer.ProduceAsync(_options.Topic, new Message<Null, string> { Value = JsonSerializer.Serialize(message) });
}

public class UserPackageIntegrationKafkaProducerOptions : KafkaProducerOptions { }