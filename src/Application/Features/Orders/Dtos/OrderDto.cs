namespace Application.Features.Orders.Dtos; 

public class OrderDto
{
    public Guid Id { get; set; }

    public OrderDto()
    {
        
    }

    public OrderDto(Guid id)
    {
        Id = id;
    }
}
