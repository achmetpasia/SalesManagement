using Application.Features.Orders.Services;
using Application.Utilities.Common.ResponseBases.Concrate;
using MediatR;

namespace Application.Features.Orders.Queries.Get
{
    public class GetOrderHandler : IRequestHandler<GetOrderQuery, ArrayBaseResponse<OrderResponseDto>>
    {
        private readonly IOrderQueryService _orderQueryService;

        public GetOrderHandler(IOrderQueryService orderQueryService)
        {
            _orderQueryService = orderQueryService;
        }

        public async Task<ArrayBaseResponse<OrderResponseDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var (query, totalCount) = await _orderQueryService.GetAll(request);

            return new ArrayBaseResponse<OrderResponseDto>(query, totalCount, request.PageLenght, request.PageIndex);
        }
    }
}
