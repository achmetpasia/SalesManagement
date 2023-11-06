using Application.Features.Orders.Commands.Create;
using Application.Features.Orders.Commands.Delete;
using Application.Features.Orders.Commands.Update;
using Application.Features.Orders.Dtos;
using Application.Utilities.Common.ResponseBases.ComplexTypes;
using Application.Utilities.Common.ResponseBases.Concrate;

namespace Application.Features.Orders.Services;

public interface IOrderCommandService
{
    Task<ObjectBaseResponse<OrderDto>> CreateAsync(CreateOrderCommand command);
    Task<ObjectBaseResponse<OrderDto>> UpdateAsync(UpdateOrderCommand command);
    Task<ResponseBase> DeleteAsync(DeleteOrderCommand command);
}
