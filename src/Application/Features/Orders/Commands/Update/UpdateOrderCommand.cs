using Application.Features.Core;
using Application.Utilities.Common.ResponseBases.Concrate;
using MediatR;

namespace Application.Features.Orders.Commands.Update;

public class UpdateOrderCommand : BaseUpdateCommand, IRequest<ObjectBaseResponse<UpdateOrderResponse>>
{
    public Guid Id { get; set; }

    public int Quantity { get; set; }

    public UpdateOrderCommand(Guid id, int quantity)
    {
        Id = id;
        Quantity = quantity;
    }
}
