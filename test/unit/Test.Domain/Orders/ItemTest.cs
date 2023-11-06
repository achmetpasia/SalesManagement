using Domain.Entites.Orders;

namespace Test.Domain.Orders;

public class ItemTest
{
    [Fact]
    public void SetQuantity_Success()
    {
        var item = new Item(3, Guid.NewGuid(), Guid.NewGuid(), 10.0m);
        int newQuantity = 5;

        item.SetQuantity(newQuantity);

        Assert.Equal(newQuantity, item.Quantity);
    }

    [Fact]
    public void SetQuantity_ShouldFail_WithNegativeQuantity()
    {
        var item = new Item(3, Guid.NewGuid(), Guid.NewGuid(), 10.0m);

        Assert.Throws<ArgumentOutOfRangeException>(() => item.SetQuantity(-2));
    }

    [Fact]
    public void SetItemPrice_Success()
    {
        var item = new Item(3, Guid.NewGuid(), Guid.NewGuid(), 10.0m); 
        decimal newItemPrice = 15.0m;

        item.SetItemPrice(newItemPrice);

        Assert.Equal(newItemPrice, item.ItemPrice);
    }

    [Fact]
    public void SetProductId_Success()
    {
        var item = new Item(3, Guid.NewGuid(), Guid.NewGuid(), 10.0m); 
        Guid newProductId = Guid.NewGuid();

        item.SetProductId(newProductId);

        Assert.Equal(newProductId, item.ProductId);
    }

    [Fact]
    public void SetOrderId_Success()
    {
        var item = new Item(3, Guid.NewGuid(), Guid.NewGuid(), 10.0m); 
        Guid newOrderId = Guid.NewGuid();

        item.SetOrderId(newOrderId);

        Assert.Equal(newOrderId, item.OrderId);
    }
}
