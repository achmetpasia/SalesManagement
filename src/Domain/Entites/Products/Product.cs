using Dawn;
using Domain.Entites.Core;
using Domain.Entites.Orders;

namespace Domain.Entites.Products;
public class Product : Entity
{
    public string Name { get; private set; }
    public decimal Price { get; private set; }

    public virtual ICollection<Item> Items { get;  set; } 

    private Product() { }

    public Product(string name, decimal price)
    {
        SetName(name);
        SetPrice(price);
    }

    public void SetName(string name)
    {
        Guard.Argument(name, nameof(name))
            .NotNull()
            .NotEmpty()
            .NotWhiteSpace();

        Name = name;
    }

    public void SetPrice(decimal price)
    {
        Guard.Argument(price, nameof(price))
            .Positive();

        Price = price;
    }
}
