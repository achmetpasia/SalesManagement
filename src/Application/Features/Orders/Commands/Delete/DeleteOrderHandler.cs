using Application.Exceptions;
using Application.Utilities.Common.ResponseBases.ComplexTypes;
using Domain.Entites.Core;
using Domain.Entites.Orders;
using MediatR;

namespace Application.Features.Orders.Commands.Delete;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, ResponseBase>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteOrderHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseBase> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await _orderRepository.FindByIdAsync(request.Id);
        if (entity == null) throw new NotFoundException("This order dont exist.");

        _orderRepository.Delete(entity);
        await _unitOfWork.SaveChangesAsync();

        return new ResponseBase() { StatusCode = System.Net.HttpStatusCode.NoContent };
    }
}
