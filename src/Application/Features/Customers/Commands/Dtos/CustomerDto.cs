namespace Application.Features.Customers.Commands.Dtos;

public class CustomerDto
{
    public Guid Id { get; set; }

    public CustomerDto()
    {
        
    }

    public CustomerDto(Guid id)
    {
        Id = id;
    }
}
