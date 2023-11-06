using Application.Features.Core;
using Application.Utilities.Common.ResponseBases.Concrate;
using MediatR;

namespace Application.Features.Products.Commands.Update;

public class UpdateProductCommand : BaseUpdateCommand, IRequest<ObjectBaseResponse<UpdateProductResponse>>
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public UpdateProductCommand(Guid id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}
