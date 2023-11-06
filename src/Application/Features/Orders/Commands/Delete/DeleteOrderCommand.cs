using Application.Features.Core;
using Application.Utilities.Common.ResponseBases.ComplexTypes;
using MediatR;

namespace Application.Features.Orders.Commands.Delete;

public class DeleteOrderCommand : BaseDeleteCommand, IRequest<ResponseBase>
{
    public DeleteOrderCommand(Guid id)
    {
        Id = id;
    }
}
