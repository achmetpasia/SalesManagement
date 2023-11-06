using Application.Features.Orders.Services;
using Application.Utilities.Common.ResponseBases.Concrate;
using AutoMapper;
using MediatR;

namespace Application.Features.Orders.Commands.Create;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, ObjectBaseResponse<CreateOrderResponse>>
{
    private readonly IOrderCommandService _orderCommandService;
    private readonly IMapper _mapper;

    public CreateOrderHandler(IOrderCommandService orderCommandService, IMapper mapper)
    {
        _orderCommandService = orderCommandService;
        _mapper = mapper;
    }

    public async Task<ObjectBaseResponse<CreateOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var createdEntity = await _orderCommandService.CreateAsync(request);

        var response = _mapper.Map<ObjectBaseResponse<CreateOrderResponse>>(createdEntity);

        return response;
    }
}
