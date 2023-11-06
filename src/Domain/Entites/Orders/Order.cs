using Dawn;
using Domain.Entites.Core;
using Domain.Entites.Customers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entites.Orders;

public class Order : Entity
{
    public DateTime OrderDate { get; private set; }
    public decimal TotalPrice { get; private set; }

    [ForeignKey("Customer")]
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    [JsonIgnore]
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    private Order()
    {
    }

    public Order(DateTime orderDate)
    {
        SetOrderDate(orderDate);
    }

    public Order(DateTime orderDate, decimal totalPrice, Guid customerId, List<Item> items)
    {
        SetOrderDate(orderDate);
        SetTotalPrice(totalPrice);
        SetCustomerId(customerId);
        AddListItems(items);
    }

    public void SetOrderDate(DateTime orderDate)
    {
        OrderDate = Guard.Argument(orderDate, nameof(orderDate))
            .NotDefault();
    }

    public void SetTotalPrice(decimal totalPrice)
    {
        Guard.Argument(totalPrice, nameof(totalPrice))
            .NotNegative();

        TotalPrice = totalPrice;
    }

    public void SetCustomerId(Guid customerId)
    {
        Guard.Argument(customerId, nameof(customerId)).NotDefault();

        CustomerId = customerId;
    }

    public void AddListItems(List<Item> items)
    {
        foreach (var item in items)
        {
            Items.Add(item);
        }
    }
}
