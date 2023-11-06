using Domain.Entites.Products;

namespace Test.Domain.Products;

public class ProductTests
{
    private Product product;

    public ProductTests()
    {
        product = new Product("Product Name", 10.0m);
    }

    [Fact]
    public void CreateProduct_Success()
    {
        product.SetName("Papper");

        Assert.Equal("Papper", product.Name);
    }

    [Fact]
    public void SetName_ShouldFail_WithInvalidValue()
    {
        Assert.Throws<ArgumentNullException>(() => product.SetName(null));
        Assert.Throws<ArgumentException>(() => product.SetName(string.Empty));
        Assert.Throws<ArgumentException>(() => product.SetName("   "));
    }

    [Fact]
    public void SetPrice_Success()
    {
        product.SetPrice(20.0m);

        Assert.Equal(20.0m, product.Price);
    }

    [Fact]
    public void SetPrice_ShouldFail_WithNegativePrice()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => product.SetPrice(-5.0m));
    }
}
