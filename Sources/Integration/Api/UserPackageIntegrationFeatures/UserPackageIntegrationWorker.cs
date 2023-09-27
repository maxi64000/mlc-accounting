using MlcAccounting.Integration.Domain.UserIntegrationAggregate;
using MlcAccounting.Integration.Domain.UserPackageIntegrationAggregate.Abstractions;

namespace MlcAccounting.Integration.Api.UserPackageIntegrationFeatures;

public class UserPackageIntegrationWorker : BackgroundService
{
    private readonly IUserPackageIntegrationConsumer _consumer;

    private readonly UserIntegrationService _service;

    public UserPackageIntegrationWorker(IUserPackageIntegrationConsumer consumer, UserIntegrationService service)
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
                var userPackageIntegration = _consumer.Consume();

                var userIntegrations = await _service.GetAllAsync(userPackageIntegration.Id);

                Parallel.ForEach(userIntegrations, async userIntegration =>
                {
                    await _service.CreateOrUpdateAsync(userIntegration);
                });

                _consumer.Commit();
            }
        }, stoppingToken);
    }
}