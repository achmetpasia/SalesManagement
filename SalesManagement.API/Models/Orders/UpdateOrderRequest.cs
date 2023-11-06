using Application.Features.Orders.Commands.Update;

namespace SalesManagement.API.Models.Orders;

public class UpdateOrderRequest
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
}
