using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using MlcAccounting.Referential.Infrastructure.Repositories;
using System.IO;

namespace MlcAccounting.Referential.Tests.Integration;

internal class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    public UserMongoRepositoryOptions UserMongoRepositoryOptions = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", true)
            .Build();

        UserMongoRepositoryOptions = config.GetSection("UserMongoRepository").Get<UserMongoRepositoryOptions>();

        base.ConfigureWebHost(builder);
    }
}