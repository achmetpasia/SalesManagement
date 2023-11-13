using Application.Features.Orders.Commands.Create;
using Application.Features.Orders.Commands.Delete;
using Application.Features.Orders.Commands.Update;
using Application.Features.Orders.Queries.Get;
using Application.Utilities.Common.ResponseBases.Concrate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesManagement.API.Models.Orders;

namespace SalesManagement.API.Controllers;

/// <summary>
/// Controller for managing order operations.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class OrderController : BaseController
{
    public OrderController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/orders
    ///     {
    ///         "customerId": "5a9ae210-0000-0000-0000-3a7d5ab8f38b",
    ///         "items": [
    ///             {
    ///                 "productId": "5a9ae210-0000-0000-0000-3a7d5ab8f38c",
    ///                 "quantity": 2
    ///             }
    ///         ]
    ///     }
    /// </remarks>
    /// <param name="request">The request payload for creating a new order.</param>
    /// <returns>
    /// An <see cref="ObjectBaseResponse{T}"/> containing information about the result of the operation.
    /// </returns>
    /// <response code="201">Returns the newly created order.</response>
    /// <response code="400">If the request is invalid or the command is null.</response>
    /// <response code="500">If an error occurred while processing the request.</response>
    [ProducesResponseType(typeof(ObjectBaseResponse<CreateOrderResponse>), 201)]
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

    /// <summary>
    /// Updates an existing order item.
    /// </summary>
    /// <param name="request">The request payload containing the item Id and quantity to update.</param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/orders
    ///     {
    ///         "Id": "e3d57674-c5be-4d6a-a34a-f237c21ae7f5",
    ///         "Quantity": 10
    ///     }
    /// 
    /// The `Id` in the request payload refers to the item Id.
    /// </remarks>
    /// <returns>
    /// An <see cref="ObjectBaseResponse{T}"/> containing information about the result of the operation.
    /// </returns>
    /// <response code="200">Returns the updated order.</response>
    /// <response code="400">If the request is invalid or the order with the given ID is not found.</response>
    /// <response code="500">If an error occurred while processing the request.</response>
    [ProducesResponseType(typeof(ObjectBaseResponse<UpdateOrderResponse>), 200)]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateOrderRequest request)
    {
        var result = await Mediator.Send(
            new UpdateOrderCommand(
            request.Id,
            request.Quantity));

        return StatusCode((int)result.StatusCode, result);
    }

    /// <summary>
    /// Deletes an order.
    /// </summary>
    /// <param name="id">The unique identifier of the order to delete.</param>
    /// <returns>
    /// No content if the deletion is successful.
    /// </returns>
    /// <response code="204">Returns no content if the deletion is successful.</response>
    /// <response code="404">If the order with the given ID is not found.</response>
    /// <response code="500">If an error occurred while processing the request.</response>
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await Mediator.Send(new DeleteOrderCommand(id));

        return NoContent();
    }

    /// <summary>
    /// Gets a list of orders based on the provided query parameters.
    /// </summary>
    /// <remarks>
    /// Use this endpoint to retrieve a list of orders based on the provided query parameters.
    /// If <paramref name="request.CustomerId"/> is specified, the endpoint will return orders only for the given customer.
    /// If <paramref name="request.CustomerId"/> is not provided, the endpoint will return orders for all customers.
    /// </remarks>
    /// <param name="request">The query parameters for retrieving orders.</param>
    /// <returns>
    /// An <see cref="ArrayBaseResponse{T}"/> containing a list of orders and the total count of matching orders.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetOrderQuery request)
    {
        var result = await Mediator.Send(request);

        return Ok(result);
    }
}
