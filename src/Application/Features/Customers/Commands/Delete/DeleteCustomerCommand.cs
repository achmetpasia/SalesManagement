using Application.Features.Core;
using Application.Utilities.Common.ResponseBases.ComplexTypes;
using MediatR;

namespace Application.Features.Customers.Commands.Delete;

public class DeleteCustomerCommand : BaseDeleteCommand, IRequest<ResponseBase>
{
    public DeleteCustomerCommand(Guid id)
    {
        Id = id;
    }
}
