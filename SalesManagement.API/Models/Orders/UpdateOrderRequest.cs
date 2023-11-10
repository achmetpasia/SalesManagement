namespace SalesManagement.API.Models.Orders;

/// <summary>
/// Represents a request to update an existing order item quantity.
/// </summary>
public class UpdateOrderRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the item to update.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the updated quantity of the order item.
    /// </summary>
    public int Quantity { get; set; }
}
