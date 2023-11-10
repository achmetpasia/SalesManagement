using Application.Features.Orders.Commands.Create;

namespace SalesManagement.API.Models.Orders;

/// <summary>
/// Represents a request to create a new order.
/// </summary>
public class CreateOrderRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the customer placing the order.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the list of items included in the order.
    /// </summary>
    public List<CreateItemRequest> Items { get; set; }
}
