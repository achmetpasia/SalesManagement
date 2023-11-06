using Application.Features.Orders.Commands.Create;

namespace SalesManagement.API.Models.Orders;

public class CreateOrderRequest
{
    public Guid CustomerId { get; set; }
    public List<CreateItemRequest> Items { get; set; }
}
