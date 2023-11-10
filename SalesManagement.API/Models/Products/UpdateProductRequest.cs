namespace SalesManagement.API.Models.Products;

/// <summary>
/// Represents a request to update an existing product.
/// </summary>
public class UpdateProductRequest
{
    /// <summary>
    /// Gets or sets the updated name of the product.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the updated price of the product.
    /// </summary>
    public decimal Price { get; set; }
}
