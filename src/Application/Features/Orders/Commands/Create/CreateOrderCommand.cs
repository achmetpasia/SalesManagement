using Application.Utilities.Common.ResponseBases.Concrate;
using MediatR;

namespace Application.Features.Orders.Commands.Create
{
    public class CreateOrderCommand : IRequest<ObjectBaseResponse<CreateOrderResponse>>
    {
        public Guid? Id { get; set; }
        public Guid CustomerId { get; set; }
        public List<CreateItemRequest> Items { get; set; }

        public CreateOrderCommand(Guid customerId, List<CreateItemRequest> items)
        {
            CustomerId = customerId;
            Items = items;
        }
    }
}
