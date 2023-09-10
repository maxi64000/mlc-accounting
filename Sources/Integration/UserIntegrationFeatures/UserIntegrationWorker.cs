using MlcAccounting.Domain.UserIntegrationAggregate;
using MlcAccounting.Domain.UserIntegrationAggregate.Abstractions;

namespace MlcAccounting.Integration.UserIntegrationFeatures;


public class UserIntegrationWorker : BackgroundService
{
    private readonly IUserIntegrationConsumer _consumer;

    private readonly UserIntegrationService _service;

    public UserIntegrationWorker(IUserIntegrationConsumer consumer, UserIntegrationService service)
    {
        _consumer = consumer;
        _service = service;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(async () =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var userIntegration = _consumer.Consume();

                await _service.CreateOrUpdateAsync(userIntegration);

                _consumer.Commit();
            }
        }, stoppingToken);
    }
}