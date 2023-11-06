using Domain.Entites.Orders;

namespace Test.Domain.Orders;

public class OrderTest
{
    [Fact]
    public void SetOrderDate_Success()
    {
        DateTime orderDate = DateTime.Now;
        var order = new Order(orderDate, 0, Guid.NewGuid(), new List<Item>());
        DateTime newOrderDate = DateTime.Now.AddDays(1);

        order.SetOrderDate(newOrderDate);

        Assert.Equal(newOrderDate, order.OrderDate);
    }

    [Fact]
    public void SetOrderDate_ShouldFail_WithDefaultDate()
    {
        var orderDate = DateTime.Now;
        var order = new Order(orderDate, 0, Guid.NewGuid(), new List<Item>());

        Assert.Throws<ArgumentException>(() => order.SetOrderDate(default(DateTime)));
    }

    [Fact]
    public void SetCustomerId_Success()
    {
        DateTime orderDate = DateTime.Now;
        var order = new Order(orderDate, 0, Guid.NewGuid(), new List<Item>());
        Guid newCustomerId = Guid.NewGuid();

        order.SetCustomerId(newCustomerId);

        Assert.Equal(newCustomerId, order.CustomerId);
    }

    [Fact]
    public void SetCustomerId_ShouldFail_WithDefaultId()
    {
        DateTime orderDate = DateTime.Now;
        var order = new Order(orderDate, 0, Guid.NewGuid(), new List<Item>());

        Assert.Throws<ArgumentException>(() => order.SetCustomerId(default(Guid)));
    }

    [Fact]
    public void SetTotalPrice_ShouldFail_WithNegativePrice()
    {
        DateTime orderDate = DateTime.Now;
        decimal initialTotalPrice = 50.0m;
        Guid customerId = Guid.NewGuid();
        List<Item> items = new List<Item>();

        var order = new Order(orderDate, initialTotalPrice, customerId, items);

        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => order.SetTotalPrice(-10.0m));
        Assert.Contains("totalPrice must be zero or greater", ex.Message);
    }

    [Fact]
    public void AddListItems_Success()
    {
        DateTime orderDate = DateTime.Now;
        var order = new Order(orderDate, 0, Guid.NewGuid(), new List<Item>());
        var items = new List<Item>
    {
        new Item(2, Guid.NewGuid(), Guid.NewGuid(), 20.0m),
        new Item(3, Guid.NewGuid(), Guid.NewGuid(), 30.0m)
    };

        order.AddListItems(items);

        Assert.Equal(items.Count, order.Items.Count);
    }
}
