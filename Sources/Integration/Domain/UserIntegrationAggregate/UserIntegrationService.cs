using MlcAccounting.Common.Integration.Entities;
using MlcAccounting.Common.Integration.Enums;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Abstractions;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Specifications;

namespace MlcAccounting.Integration.Domain.UserIntegrationAggregate;

public class UserIntegrationService
{
    private readonly IUserIntegrationRepository _repository;

    private readonly IUserIntegrationProducer _producer;

    private readonly IUserReferentialClient _client;

    public UserIntegrationService(IUserIntegrationRepository repository, IUserIntegrationProducer producer, IUserReferentialClient client)
    {
        _repository = repository;
        _producer = producer;
        _client = client;
    }

    public async Task<IEnumerable<UserIntegration>> GetAllAsync(UserIntegrationSpecification specification) => await _repository.GetAllAsync(specification);

    public async Task<IEnumerable<UserIntegration>> GetAllAsync(Guid packageId) => await _repository.GetAllAsync(packageId);

    public async Task<UserIntegration?> GetAsync(Guid id) => await _repository.GetAsync(id);

    public async Task<Guid> SubmitAsync(UserIntegration userIntegration)
    {
        await _repository.CreateAsync(userIntegration);

        await _producer.ProduceAsync(userIntegration);

        return userIntegration.Id;
    }

    public async Task CreateOrUpdateAsync(UserIntegration userIntegration)
    {
        var commentaries = new List<Commentary>();

        if (string.IsNullOrWhiteSpace(userIntegration.Name))
        {
            userIntegration.Status = IntegrationStatus.Refused;
            commentaries.Add(new Commentary(CommentaryType.Error, "The name field is mandatory."));
        }

        if (string.IsNullOrWhiteSpace(userIntegration.Password))
        {
            userIntegration.Status = IntegrationStatus.Refused;
            commentaries.Add(new Commentary(CommentaryType.Error, "The password field is mandatory."));
        }

        if (userIntegration.Status != IntegrationStatus.Refused)
        {
            var users = await _client.GetAllAsync(userIntegration.Name!);

            if (users.Any())
            {
                var user = users.First();

                user.Password = userIntegration.Password!;

                await _client.UpdateAsync(user);

                commentaries.Add(new Commentary(CommentaryType.Information, "The user has been updated."));
            }
            else
            {
                await _client.CreateAsync(new User(userIntegration.Name!, userIntegration.Password!));

                commentaries.Add(new Commentary(CommentaryType.Information, "The user has been created."));
            }

            userIntegration.Status = IntegrationStatus.Accepted;
        }

        userIntegration.Commentaries = commentaries;
        userIntegration.TreatedAt = DateTime.Now;

        await _repository.UpdateAsync(userIntegration);
    }
}