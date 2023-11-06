using Application.Features.Orders.Services;
using Application.Utilities.Common.ResponseBases.ComplexTypes;
using MediatR;

namespace Application.Features.Orders.Commands.Delete;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, ResponseBase>
{
    private readonly IOrderCommandService _orderCommandService;

    public DeleteOrderHandler(IOrderCommandService orderCommandService)
    {
        _orderCommandService = orderCommandService;
    }

    public async Task<ResponseBase> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var success = await _orderCommandService.DeleteAsync(request);

        return success;
    }
}
