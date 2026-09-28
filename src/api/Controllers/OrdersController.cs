using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using TEcomerc.Application.Commands;
using TEcomerc.Domain.Entities;

namespace TEcomerc.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
sealed class OrdersController(ISender mediator, ILogger<OrdersController>? log) : ControllerBase
{
    [HttpGet("{id:guid}", Name = nameof(GetById))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        log?.LogInformation("Lendo ordem por id: `{Id}`", id);
        var result = await mediator.Send(new ReadOrderByIdCommand(id), ct);
        return result is not null ? Ok(result) : NotFound(new { Message = $"Order with ID {id} was not found." });
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async IAsyncEnumerable<OrderValues<OrderItemValues>> List(
        [FromQuery] int page, [FromQuery] int pageSize,
        [EnumeratorCancellation] CancellationToken ct,
        [FromQuery] bool includeItems = false

    ){
        var result = await mediator.Send(new ListOrderQuery(page, pageSize, includeItems), ct);
        await foreach (var item in result)
        {
            yield return item.ToValues();
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Post([FromBody] OrderValues<OrderItemValues> order, CancellationToken ct)
    {
        var response = await mediator.Send(new CreateOrderCommand(new OrderValues<OrderItem>(
            Id: order.Id,
            CustomerId: order.CustomerId,
            Status: order.Status,
            CreatedAt: order.CreatedAt,
            Items: order.Items.Cast<OrderItem>()
        )), ct);

        return Ok(response);
    }


    [HttpPatch("{id:guid}/cancel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken ct)
    {
        await mediator.Send(new CancelOrderCommand(id), ct);
        return Ok();
    }



}