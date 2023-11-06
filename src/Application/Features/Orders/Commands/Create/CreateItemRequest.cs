namespace Application.Features.Orders.Commands.Create;

public class CreateItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public CreateItemRequest(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}
