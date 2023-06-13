using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using MlcAccounting.Domain.UserAggregate.Abstractions;
using MlcAccounting.Infrastructure.UserRepository;

namespace MlcAccounting.Api.Tests.Integration;

internal class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton<IUserRepository, UserInMemoryRepository>();
        });

        base.ConfigureWebHost(builder);
    }
}