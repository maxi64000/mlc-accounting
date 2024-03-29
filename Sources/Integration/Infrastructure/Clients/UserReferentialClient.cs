﻿using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using MlcAccounting.Common.Client;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Abstractions;
using MlcAccounting.Integration.Domain.UserIntegrationAggregate.Entities;

namespace MlcAccounting.Integration.Infrastructure.Clients;

public class UserReferentialClient : IUserReferentialClient
{
    private readonly UserReferentialClientOptions _options;

    public UserReferentialClient(IOptions<UserReferentialClientOptions> options)
    {
        _options = options.Value;
    }

    public async Task<IEnumerable<User>> GetAllAsync(string name) =>
        await _options.BaseUrl
            .AppendPathSegment("users")
            .SetQueryParam("name", name)
            .GetJsonAsync<IEnumerable<User>>();

    public async Task CreateAsync(User user) =>
        await _options.BaseUrl
            .AppendPathSegment("users")
            .PostJsonAsync(user);

    public async Task UpdateAsync(User user) =>
        await _options.BaseUrl
            .AppendPathSegment("users")
            .AppendPathSegment(user.Id)
            .PutJsonAsync(user);
}

public class UserReferentialClientOptions : ClientOptions { }