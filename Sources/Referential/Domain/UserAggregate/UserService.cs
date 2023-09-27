using MlcAccounting.Referential.Domain.UserAggregate.Abstractions;
using MlcAccounting.Referential.Domain.UserAggregate.Entities;
using MlcAccounting.Referential.Domain.UserAggregate.Specifications;

namespace MlcAccounting.Referential.Domain.UserAggregate;

public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<User>> GetAllAsync(UserSpecification specification) => await _repository.GetAllAsync(specification);

    public async Task<User?> GetAsync(Guid id) => await _repository.GetAsync(id);

    public async Task<Guid?> CreateAsync(User user)
    {
        var result = await _repository.GetAllAsync(user.Name);

        if (result.Any())
        {
            return null;
        }

        await _repository.CreateAsync(user);

        return user.Id;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var current = await _repository.GetAsync(user.Id);

        if (current == null)
        {
            return false;
        }

        user.CreatedAt = current.CreatedAt;
        user.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(user);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var current = await _repository.GetAsync(id);

        if (current == null)
        {
            return false;
        }

        await _repository.DeleteAsync(id);

        return true;
    }
}