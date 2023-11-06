using Dawn;
using Domain.Entites.Core;
using Domain.Entites.Products;
using System.Text.Json.Serialization;

namespace Domain.Entites.Orders;

public class Item : Entity
{
    public int Quantity { get; private set; }
    public decimal ItemPrice { get; set; }

    public Guid ProductId { get; set; }
    [JsonIgnore]
    public virtual Product Product { get; set; }
    public Guid OrderId { get; set; }
    [JsonIgnore]
    public virtual Order Order { get; set; }

    private Item() { }


    public Item(int quantity, Guid productId, Guid orderId, decimal itemPrice) 
    {
        SetQuantity(quantity);
        SetProductId(productId);
        SetOrderId(orderId);
        SetItemPrice(itemPrice);
    }

    public void SetQuantity(int quantity)
    {
         Guard.Argument(quantity, nameof(quantity))
           .NotNegative();

        Quantity = quantity;
    }

    public void SetItemPrice(decimal itemPrice)
    {
        Guard.Argument(itemPrice, nameof(itemPrice))
           .NotNegative();

        ItemPrice = itemPrice;
    }

    public void SetProductId(Guid productId)
    {
        Guard.Argument(productId, nameof(productId)).NotDefault();

        ProductId = productId;
    }

    public void SetOrderId(Guid orderId)
    {
        Guard.Argument(orderId, nameof(orderId)).NotDefault();

        OrderId = orderId;
    }
}
