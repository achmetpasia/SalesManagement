using Application.Utilities.Common.ResponseBases.Concrate;
using MediatR;

namespace Application.Features.Products.Commands.Create;

public class CreateProductCommand : IRequest<ObjectBaseResponse<CreateProductResponse>>
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public CreateProductCommand(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}
