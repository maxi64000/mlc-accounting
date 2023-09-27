using AspNetCore.Proxy;
using Microsoft.AspNetCore.Mvc;

namespace MlcAccounting.Api.Controllers;

[ApiController]
[Route("user-integrations")]
public class UserIntegrationController : ControllerBase
{
    [HttpGet]
    public Task GetAllUserIntegrations() =>
        this.HttpProxyAsync($"http://localhost:5082/user-integrations{Request.QueryString}");

    [HttpGet("{id:guid}")]
    public Task GetUserIntegration([FromRoute] Guid id) =>
        this.HttpProxyAsync($"http://localhost:5082/user-integrations{id}");

    [HttpPost("")]
    public Task SubmitUserIntegration() =>
        this.HttpProxyAsync($"http://localhost:5082/user-integrations");
}