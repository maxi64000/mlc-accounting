using MediatR;
using Microsoft.AspNetCore.Mvc;
using MlcAccounting.Api.UserFeatures.GetUser;
using MlcAccounting.Domain.UserAggregate.Entities;
using System.Net;

namespace MlcAccounting.Api.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> GetUser([FromRoute] Guid id)
    {
        var user = await _mediator.Send(new GetUserQuery { Id = id });

        return user != null ? Ok(user) : NotFound(new ProblemDetails
        {
            Title = "Not Found",
            Status = (int)HttpStatusCode.NotFound,
            Detail = "This user doesn't exist."
        });
    }
}