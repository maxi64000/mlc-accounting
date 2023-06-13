﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MlcAccounting.Api.UserFeatures.CreateUser;
using MlcAccounting.Api.UserFeatures.GetUser;
using MlcAccounting.Api.UserFeatures.UpdateUser;
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

    [HttpPost("")]
    public async Task<ActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var id = await _mediator.Send(command);

        if (id == null)
        {
            return Conflict(new ProblemDetails
            {
                Title = "Conflict",
                Status = (int)HttpStatusCode.Conflict,
                Detail = "This user already exist."
            });
        }

        return CreatedAtAction(nameof(GetUser), new { id }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserCommand command)
    {
        command.Id = id;

        return await _mediator.Send(command)
            ? NoContent()
            : NotFound(new ProblemDetails
            {
                Title = "Not Found",
                Status = (int)HttpStatusCode.NotFound,
                Detail = "This user doesn't exist."
            });
    }
}