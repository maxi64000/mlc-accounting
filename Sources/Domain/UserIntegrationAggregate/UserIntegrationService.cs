using MlcAccounting.Domain.UserIntegrationAggregate.Abstractions;
using MlcAccounting.Domain.UserIntegrationAggregate.Entities;
using MlcAccounting.Domain.UserIntegrationAggregate.Enums;
using MlcAccounting.Domain.UserIntegrationAggregate.Specifications;

namespace MlcAccounting.Domain.UserIntegrationAggregate;

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
            //var users = await _client.GetAllAsync(userIntegration.Name!);

            var users = new List<User>();

            if (users.Any())
            {
                var user = users.First();

                user.Password = userIntegration.Password!;

                //await _client.UpdateAsync(user);

                commentaries.Add(new Commentary(CommentaryType.Information, "The user has been updated."));
            }
            else
            {
                //await _client.CreateAsync(new User(userIntegration.Name!, userIntegration.Password!));

                commentaries.Add(new Commentary(CommentaryType.Information, "The user has been created."));
            }

            userIntegration.Status = IntegrationStatus.Accepted;
        }

        userIntegration.Commentaries = commentaries;

        await _repository.UpdateAsync(userIntegration);
    }
}