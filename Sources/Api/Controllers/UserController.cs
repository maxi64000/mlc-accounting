using AspNetCore.Proxy;
using Microsoft.AspNetCore.Mvc;
using MlcAccounting.Common.Enums;
using MlcAccounting.Referential.Domain.UserAggregate.Entities;

namespace MlcAccounting.Api.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
    public Task GetAllUsers([FromQuery] GetAllUsersQuery _) =>
        this.HttpProxyAsync($"http://localhost:5080/users{Request.QueryString}");

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    public Task GetUser([FromRoute] Guid id) =>
        this.HttpProxyAsync($"http://localhost:5080/users/{id}");
}

public class GetAllUsersQuery
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public string? SortBy { get; set; }

    public OrderType? OrderBy { get; set; }

    public string? Name { get; set; }
}