namespace Application.Features.Products.Dtos;

public class ProductDto
{
    public Guid Id { get; set; }

    public ProductDto()
    {
        
    }

    public ProductDto(Guid id)
    {
        Id = id;
    }

}
