﻿using MlcAccounting.Domain.UserAggregate.Entities;

namespace MlcAccounting.Domain.UserAggregate;

public interface IUserService
{
    Task<User?> GetAsync(Guid id);

    Task<Guid?> CreateAsync(User user);

    Task<bool> UpdateAsync(User user);

    Task<bool> DeleteAsync(Guid id);
}