using Application.Features.Core;
using Application.Utilities.Common.ResponseBases.ComplexTypes;
using MediatR;

namespace Application.Features.Products.Commands.Delete;

public class DeleteProductCommand : BaseDeleteCommand, IRequest<ResponseBase>
{
    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }
}
