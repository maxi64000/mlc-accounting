using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.IO;
using MlcAccounting.Integration.Infrastructure.Repositories;

namespace MlcAccounting.Integration.Tests.Integration;

internal class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    public UserIntegrationMongoRepositoryOptions UserIntegrationMongoRepositoryOptions = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", true)
            .Build();

        UserIntegrationMongoRepositoryOptions = config.GetSection("UserIntegrationMongoRepository").Get<UserIntegrationMongoRepositoryOptions>();

        base.ConfigureWebHost(builder);
    }
}