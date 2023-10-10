using MediatR;
using Microsoft.AspNetCore.Mvc;
using MlcAccounting.Referential.Domain.ProductAggregate.Entities;
using MlcAccounting.Referential.Domain.ProductAggregate.Features.CreateProduct;
using MlcAccounting.Referential.Domain.ProductAggregate.Features.GetProduct;
using System.Net;

namespace MlcAccounting.Referential.Api.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Product>> GetProduct([FromRoute] Guid id)
    {
        var result = await _mediator.Send(new GetProductQuery { Id = id });

        return result != null ? Ok(result) : NotFound(new ProblemDetails
        {
            Title = "Not Found",
            Status = (int)HttpStatusCode.NotFound,
            Detail = "This product doesn't exist."
        });
    }

    [HttpPost("")]
    public async Task<ActionResult> CreateUser([FromBody] CreateProductCommand command)
    {
        var id = await _mediator.Send(command);

        if (id == null)
        {
            return Conflict(new ProblemDetails
            {
                Title = "Conflict",
                Status = (int)HttpStatusCode.Conflict,
                Detail = "This product already exist."
            });
        }

        return CreatedAtAction(nameof(GetProduct), new { id }, null);
    }
}