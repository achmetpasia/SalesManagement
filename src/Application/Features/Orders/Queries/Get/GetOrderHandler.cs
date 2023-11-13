using Application.Utilities.Common.ResponseBases.Concrate;
using Domain.Entites.Orders;
using MediatR;

namespace Application.Features.Orders.Queries.Get
{
    public class GetOrderHandler : IRequestHandler<GetOrderQuery, ArrayBaseResponse<OrderResponseDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ArrayBaseResponse<OrderResponseDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var list = new List<Order>();

            if (request.CustomerId != Guid.Empty)
            {
                list = await _orderRepository.FindAllByConditionAsync(x => x.CustomerId == request.CustomerId);
            }
            else
            {
                list = await _orderRepository.FindAllAsync();
            }

            if (request.StartDate.HasValue)
            {
                list = list.Where(x => x.CreatedDate >= request.StartDate).ToList();
            }
            if (request.EndDate.HasValue)
            {
                list = list.Where(x => x.CreatedDate <= request.EndDate).ToList();
            }


            var totalCount = list.Count();

            list = list.OrderByDescending(x => x.CreatedDate).ToList();

            OrderResponseDto data = new OrderResponseDto();

            var response = new List<OrderResponseDto>
            {
                new OrderResponseDto
                {
                    Orders = list
                }

            };

            return new ArrayBaseResponse<OrderResponseDto>(response, totalCount, request.PageLenght, request.PageIndex);
        }
    }
}
