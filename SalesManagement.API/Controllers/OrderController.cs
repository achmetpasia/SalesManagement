using Application.Features.Orders.Commands.Create;
using Application.Features.Orders.Commands.Delete;
using Application.Features.Orders.Commands.Update;
using Application.Features.Orders.Queries.Get;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.API.Models.Orders;

namespace SalesManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        var result = await Mediator.Send(
            new CreateOrderCommand(
                request.CustomerId,
                request.Items
                ));

        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateOrderRequest request)
    {
        var result = await Mediator.Send(
            new UpdateOrderCommand(
            request.Id,
            request.Quantity));

        return StatusCode((int)result.StatusCode, request);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await Mediator.Send(new DeleteOrderCommand(id));

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetOrderQuery request)
    {
        var result = await Mediator.Send(request);

        return Ok(result);
    }
}
