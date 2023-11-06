using Application.Features.Orders.Services;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using MediatR;

namespace Application.Features.Orders.Commands.Update
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, ObjectBaseResponse<UpdateOrderResponse>>
    {
        private readonly IOrderCommandService _orderCommandService;
        private readonly IMapper _mapper;

        public UpdateOrderHandler(IOrderCommandService orderCommandService, IMapper mapper)
        {
            _orderCommandService = orderCommandService;
            _mapper = mapper;
        }

        public async Task<ObjectBaseResponse<UpdateOrderResponse>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await _orderCommandService.UpdateAsync(request);

            var response = _mapper.Map<ObjectBaseResponse<UpdateOrderResponse>>(entity);

            return response;
        }
    }
}
